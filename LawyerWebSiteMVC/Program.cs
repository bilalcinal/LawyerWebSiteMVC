using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DNTCaptcha.Core;
using FluentValidation.AspNetCore;
using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Autofac Dependency Injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    // Register your own things directly with Autofac here.
    builder.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.AccessDeniedPath = new PathString("/cms/account/login/");
    options.LoginPath = new PathString("/cms/account/login/");
    options.LogoutPath = new PathString("/cms/account/logout/");
    options.Cookie.Name = ".KonuralpBungalow.net";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDNTCaptcha(options =>
{
    options.UseCookieStorageProvider()
    .AbsoluteExpiration(minutes: 7)
    .ShowThousandsSeparators(false)
    .WithEncryptionKey("KonuralpBungalow Secure Key")
    .InputNames(new DNTCaptchaComponent
    {
        CaptchaHiddenInputName = "DNT_CaptchaText",
        CaptchaHiddenTokenName = "DNT_CaptchaToken",
        CaptchaInputName = "DNT_CaptchaInputText"
    })
    .Identifier("dnt_Captcha");
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddWebEncoders(o => {
    o.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});

builder.Services.AddControllersWithViews().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Register services
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILetterService, LetterService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAboutService, AboutService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
};
app.UseCookiePolicy(cookiePolicyOptions);
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
