using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    public class LetterController : Controller
    {
        private readonly ILetterService _letterService;

        public LetterController(ILetterService letterService)
        {
            _letterService = letterService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Letter letter)
        {
            if (ModelState.IsValid)
            {
                var (success, message) = await _letterService.CreateLetterAsync(letter);
                if (success)
                {
                    TempData["Success"] = message;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", message);
            }
            return View(letter);
        }
    }
}
