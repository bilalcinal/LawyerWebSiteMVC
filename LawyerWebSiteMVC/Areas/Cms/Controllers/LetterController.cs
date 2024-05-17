using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Route("[controller]")]
    [Area("Cms")]
     public class LetterController : Controller
    {
        private readonly ILetterService _letterService;

        public LetterController(ILetterService letterService)
        {
            _letterService = letterService;
        }

        public async Task<IActionResult> Index()
        {
            var letters = await _letterService.GetAllLettersAsync();
            return View(letters);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Letter letter)
        {
            var result = await _letterService.CreateLetterAsync(letter);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(letter);
        }
    }
}