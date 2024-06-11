using LawyerWebSiteMVC.Data;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Interface
{
    public interface IAboutService
    {
        Task<About> GetAboutAsync();
        Task<(bool, string)> CreateOrUpdateAboutAsync(About about);
        Task<bool> DeleteAboutAsync(int id);
    }
}
