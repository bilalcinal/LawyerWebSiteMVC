using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Models;

namespace LawyerWebSiteMVC.Interface;
public interface IArticleService
{
    Task<(bool, string)> CreateArticleAsync(ArticleDto articleDto);
    Task<(bool, string)> UpdateArticleAsync(ArticleDto articleDto);
    Task<bool> DeleteArticleAsync(int articleId);
    Task<Article> GetArticleByIdAsync(int articleId);
    Task<IEnumerable<Article>> GetAllArticlesAsync();
    Task<Article> GetArticleByIdWithCategoryAsync(int id);

    Task<(bool, string)> CreateCommentAsync(Comment comment);
    Task<bool> DeleteCommentAsync(int commentId);
    Task<IEnumerable<Comment>> GetCommentsByArticleIdAsync(int articleId);
}