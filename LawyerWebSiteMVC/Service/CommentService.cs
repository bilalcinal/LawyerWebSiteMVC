using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Service
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool, string)> CreateCommentAsync(Comment comment)
        {
            comment.Status = false; // Yorum oluşturulurken varsayılan olarak false ayarlanıyor
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return (true, "Comment created successfully");
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.Include(c => c.Article).ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments.FindAsync(commentId);
        }

        public async Task<(bool, string)> UpdateCommentStatusAsync(int commentId, bool status)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return (false, "Comment not found");
            }

            comment.Status = status;
            await _context.SaveChangesAsync();
            return (true, "Comment status updated successfully");
        }
    }
}
