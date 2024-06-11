using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var about = await _aboutService.GetAboutAsync();
            return View(about);
        }
    }
}
