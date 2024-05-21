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

            foreach (var formFile in articleDto.ArticlePhotos)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    var photo = new ArticlePhoto
                    {
                        ArticleId = articleDto.Article.Id,
                        Image = memoryStream.ToArray()
                    };
                    _context.ArticlePhotos.Add(photo);
                }
            }

            await _context.SaveChangesAsync();

            return (true, "Article created successfully");
        }

        public async Task<(bool, string)> UpdateArticleAsync(ArticleDto articleDto)
        {
            var existingArticle = await _context.Articles
                .Where(a => !a.IsDeleted)
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == articleDto.Article.Id);

            if (existingArticle == null)
                return (false, "Article not found");

            existingArticle.Title = articleDto.Article.Title;
            existingArticle.Subtitle = articleDto.Article.Subtitle;
            existingArticle.Content = articleDto.Article.Content;
            existingArticle.link = articleDto.Article.link;

            if (articleDto.ArticlePhotos != null && articleDto.ArticlePhotos.Any())
            {
                _context.ArticlePhotos.RemoveRange(existingArticle.ArticlePhotos);

                foreach (var formFile in articleDto.ArticlePhotos)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        var photo = new ArticlePhoto
                        {
                            ArticleId = existingArticle.Id,
                            Image = memoryStream.ToArray()
                        };
                        _context.ArticlePhotos.Add(photo);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return (true, "Article updated successfully");
        }

       public async Task<bool> DeleteArticleAsync(int articleId)
        {
            var article = await _context.Articles
                .Where(a => !a.IsDeleted)
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                return false;

            article.IsDeleted = true;

            foreach (var photo in article.ArticlePhotos)
            {
                photo.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Article> GetArticleByIdAsync(int articleId)
        {
            return await _context.Articles
                .Where(a => !a.IsDeleted)
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == articleId);
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles
                .Where(a => !a.IsDeleted)
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
