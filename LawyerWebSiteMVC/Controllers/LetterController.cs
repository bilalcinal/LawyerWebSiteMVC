using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly ILetterService _letterService;

        public LetterController(ILetterService letterService)
        {
            _letterService = letterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLetter(Letter letter)
        {
            var result = await _letterService.CreateLetterAsync(letter);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLetters()
        {
            var letters = await _letterService.GetAllLettersAsync();
            return Ok(letters);
        }
    }
}
