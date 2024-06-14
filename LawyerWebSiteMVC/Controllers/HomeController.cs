using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISliderService _sliderService;
        private readonly IArticleService _articleService;

        public HomeController(ILogger<HomeController> logger, ISliderService sliderService, IArticleService articleService)
        {
            _sliderService = sliderService;
            _logger = logger;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            var latestArticles = (await _articleService.GetLatestArticlesAsync(0, 12)).ToList();
            var viewModel = new HomeViewModel
            {
                Sliders = sliders,
                LatestArticles = latestArticles
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
