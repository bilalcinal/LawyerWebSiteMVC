using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace LawyerWebSiteMVC.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<AppUser>> SignInAsync(AppUser appUser)
        {
            var row = _context.AppUsers.FirstOrDefault(x => x.Email == appUser.Email.Trim() && x.Password == appUser.Password.Trim());
            if (row != null)
            {
                var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, row.FullName),
                    new Claim(ClaimTypes.Email, row.Email),
                    new Claim(ClaimTypes.Role, row.Role)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(1),
                    IsPersistent = true,
                    AllowRefresh = false
                });

                return new SuccessDataResult<AppUser>(row, Messages.SuccessfulLogin);
            }

            return new ErrorDataResult<AppUser>(null, Messages.UserNotFound);
        }
    }
}
