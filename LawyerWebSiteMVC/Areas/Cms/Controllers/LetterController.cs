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

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var letter = await _letterService.GetLetterByIdAsync(id);
            if (letter == null)
            {
                return NotFound();
            }
            return View(letter);
        }
    }
}
