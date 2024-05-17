using LawyerWebSiteMVC.Data;
namespace LawyerWebSiteMVC.Interface;
public interface ISliderService
{
    Task<(bool, string)> CreateSliderAsync(Slider slider);
    Task<(bool, string)> UpdateSliderAsync(Slider slider);
    Task<bool> DeleteSliderAsync(int sliderId);
    Task<Slider> GetSliderByIdAsync(int sliderId);
    Task<IEnumerable<Slider>> GetAllSlidersAsync();
}