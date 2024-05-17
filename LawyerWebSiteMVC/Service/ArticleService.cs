using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Models;
using Microsoft.EntityFrameworkCore;
namespace LawyerWebSiteMVC.Service
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool, string)> CreateArticleAsync(ArticleDto articleDto)
        {
            _context.Articles.Add(articleDto.Article);
            await _context.SaveChangesAsync();

            foreach (var photo in articleDto.ArticlePhotos)
            {
                photo.ArticleId = articleDto.Article.Id;
                _context.ArticlePhotos.Add(photo);
            }
            
            await _context.SaveChangesAsync();

            return (true, "Article created successfully");
        }

        public async Task<(bool, string)> UpdateArticleAsync(ArticleDto articleDto)
        {
            var existingArticle = await _context.Articles
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == articleDto.Article.Id);

            if (existingArticle == null)
                return (false, "Article not found");

            existingArticle.Title = articleDto.Article.Title;
            existingArticle.Subtitle = articleDto.Article.Subtitle;
            existingArticle.Content = articleDto.Article.Content;
            existingArticle.link = articleDto.Article.link;

            _context.ArticlePhotos.RemoveRange(existingArticle.ArticlePhotos);
            foreach (var photo in articleDto.ArticlePhotos)
            {
                photo.ArticleId = existingArticle.Id;
                _context.ArticlePhotos.Add(photo);
            }

            await _context.SaveChangesAsync();

            return (true, "Article updated successfully");
        }

        public async Task<bool> DeleteArticleAsync(int articleId)
        {
            var article = await _context.Articles
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                return false;

            _context.ArticlePhotos.RemoveRange(article.ArticlePhotos);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Article> GetArticleByIdAsync(int articleId)
        {
            return await _context.Articles
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == articleId);
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles
                .Include(a => a.ArticlePhotos)
                .ToListAsync();
        }

        public async Task<(bool, string)> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return (true, "Comment created successfully");
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null)
                return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByArticleIdAsync(int articleId)
        {
            return await _context.Comments
                .Where(c => c.ArticleId == articleId)
                .ToListAsync();
        }
    }
}
