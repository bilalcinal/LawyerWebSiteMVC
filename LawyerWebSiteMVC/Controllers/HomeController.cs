using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LawyerWebSiteMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISliderService _sliderService;


        public HomeController(ILogger<HomeController> logger , ISliderService sliderService)
        {
            _sliderService = sliderService;
            _logger = logger;
        }

         public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            return View(sliders);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
