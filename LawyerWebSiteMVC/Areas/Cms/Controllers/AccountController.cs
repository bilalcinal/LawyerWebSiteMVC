using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace LawyerWebSite.UI.Areas.Cms.Controllers;
[Area("Cms")]
public class AccountController : Controller
{
    private readonly IAppUserService _appUserService;
    public AccountController(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    [Route("/cms/account/login/")]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [Route("/cms/account/Login/")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AppUser appUser, string returnUrl = null)
    {
        var user = await _appUserService.SignInAsync(appUser);
        if (!user.Success)
        {
            TempData["Error"] = user.Message;
            return View(appUser);
        }
        TempData["Success"] = user.Message;
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
    }

    [Route("/cms/account/logout/")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
