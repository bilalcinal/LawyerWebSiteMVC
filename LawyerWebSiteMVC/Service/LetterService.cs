using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Service
{
    public class LetterService : ILetterService
    {
        private readonly ApplicationDbContext _context;

        public LetterService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<(bool, string)> CreateLetterAsync(Letter letter)
        {
            _context.Letters.Add(letter);
            await _context.SaveChangesAsync();
            return (true, "Letter created successfully");
        }

        public async Task<IEnumerable<Letter>> GetAllLettersAsync()
        {
            return await _context.Letters.ToListAsync();
        }

        public async Task<Letter> GetLetterByIdAsync(int id)
        {
            return await _context.Letters.FindAsync(id);
        }
    }
}
