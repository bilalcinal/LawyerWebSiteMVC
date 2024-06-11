using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Route("Cms/[controller]")]
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

        [HttpGet]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate()
        {
            var about = await _aboutService.GetAboutAsync() ?? new About();
            return View(about);
        }

        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(About about)
        {
            if (ModelState.IsValid)
            {
                var result = await _aboutService.CreateOrUpdateAboutAsync(about);
                if (result.Item1)
                    return RedirectToAction("Index");

                ModelState.AddModelError(string.Empty, result.Item2);
            }
            return View(about);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _aboutService.DeleteAboutAsync(id);
            if (result)
                return RedirectToAction("Index");

            return View("Error");
        }
    }
}
