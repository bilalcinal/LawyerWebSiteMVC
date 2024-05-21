using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            articleDto.Article.Content = JsonConvert.SerializeObject(articleDto.Article.Content);
            _context.Articles.Add(articleDto.Article);
            await _context.SaveChangesAsync();

            if (articleDto.ArticlePhotos != null)
            {
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
            existingArticle.Content = JsonConvert.SerializeObject(articleDto.Article.Content);
            existingArticle.link = articleDto.Article.link;
            existingArticle.CategoryId = articleDto.Article.CategoryId;

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
            var article = await _context.Articles
                .Where(a => !a.IsDeleted)
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            article.Content = TryDeserializeContent(article.Content);
            return article;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            var articles = await _context.Articles
                .Where(a => !a.IsDeleted)
                .Include(a => a.Category) 
                .Include(a => a.ArticlePhotos)
                .ToListAsync();

            articles.ForEach(a => a.Content = TryDeserializeContent(a.Content));
            return articles;
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
    }
}
