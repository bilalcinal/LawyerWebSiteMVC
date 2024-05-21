using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Route("Cms/[controller]")]
    public class LetterController : Controller
    {
        private readonly ILetterService _letterService;

        public LetterController(ILetterService letterService)
        {
            _letterService = letterService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var letters = await _letterService.GetAllLettersAsync();
            return View(letters);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Letter letter)
        {
            var result = await _letterService.CreateLetterAsync(letter);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(letter);
        }
    }
}
