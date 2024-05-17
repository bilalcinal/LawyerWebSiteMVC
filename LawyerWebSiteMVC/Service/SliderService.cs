using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.EntityFrameworkCore;

namespace LawyerWebSiteMVC.Service;

public class SliderService : ISliderService
    {
        private readonly ApplicationDbContext _context;

        public SliderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool, string)> CreateSliderAsync(Slider slider)
        {
            _context.Sliders.Add(slider);
            await _context.SaveChangesAsync();
            return (true, "Slider created successfully");
        }

        public async Task<(bool, string)> UpdateSliderAsync(Slider slider)
        {
            var existingSlider = await _context.Sliders.FindAsync(slider.Id);

            if (existingSlider == null)
                return (false, "Slider not found");

            existingSlider.Image = slider.Image;
            existingSlider.Content = slider.Content;
            existingSlider.MyProperty = slider.MyProperty;
            await _context.SaveChangesAsync();

            return (true, "Slider updated successfully");
        }

        public async Task<bool> DeleteSliderAsync(int sliderId)
        {
            var slider = await _context.Sliders.FindAsync(sliderId);

            if (slider == null)
                return false;

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Slider> GetSliderByIdAsync(int sliderId)
        {
            return await _context.Sliders.FindAsync(sliderId);
        }

        public async Task<IEnumerable<Slider>> GetAllSlidersAsync()
        {
            return await _context.Sliders.ToListAsync();
        }
    }