using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.EntityFrameworkCore;

namespace LawyerWebSiteMVC.Service;

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
    }