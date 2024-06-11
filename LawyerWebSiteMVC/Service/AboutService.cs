using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Service
{
    public class AboutService : IAboutService
    {
        private readonly ApplicationDbContext _context;

        public AboutService(ApplicationDbContext context)
        {
            _context = context;
        }

         public async Task<About> GetAboutAsync()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            if (about != null)
            {
                about.Content = TryDeserializeContent(about.Content);
            }
            return about;
        }

        private string TryDeserializeContent(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<string>(content);
            }
            catch (JsonReaderException)
            {
                return content;
            }
        }

        public async Task<(bool, string)> CreateOrUpdateAboutAsync(About about)
        {
            var existingAbout = await GetAboutAsync();
            if (existingAbout == null)
            {
                about.Content = JsonConvert.SerializeObject(about.Content);
                _context.Abouts.Add(about);
            }
            else
            {
                existingAbout.Title = about.Title;
                existingAbout.Content = JsonConvert.SerializeObject(about.Content);
            }
            await _context.SaveChangesAsync();
            return (true, "About section saved successfully");
        }

        public async Task<bool> DeleteAboutAsync(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
                return false;

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
