using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Route("Cms/[controller]")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            return View(sliders);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Slider slider, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    slider.Image = memoryStream.ToArray();
                }
            }

            var result = await _sliderService.CreateSliderAsync(slider);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(slider);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Slider slider, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    slider.Image = memoryStream.ToArray();
                }
            }

            var result = await _sliderService.UpdateSliderAsync(slider);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(slider);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }

        [HttpPost("Delete/{id}")]
        [ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _sliderService.DeleteSliderAsync(id);
            if (result)
                return RedirectToAction("Index");
            return View("Error");
        }
    }
}
