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
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            var result = await _sliderService.CreateSliderAsync(slider);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(slider);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Slider slider)
        {
            var result = await _sliderService.UpdateSliderAsync(slider);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(slider);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _sliderService.DeleteSliderAsync(id);
            if (result)
                return RedirectToAction("Index");
            return View();
        }
    }
}