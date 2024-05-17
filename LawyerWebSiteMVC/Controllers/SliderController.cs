using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(Slider slider)
        {
            var result = await _sliderService.CreateSliderAsync(slider);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSlider(Slider slider)
        {
            var result = await _sliderService.UpdateSliderAsync(slider);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var result = await _sliderService.DeleteSliderAsync(id);
            if (result)
                return Ok("Slider deleted successfully");
            return NotFound("Slider not found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSliderById(int id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            if (slider != null)
                return Ok(slider);
            return NotFound("Slider not found");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSliders()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            return Ok(sliders);
        }
    }
}
