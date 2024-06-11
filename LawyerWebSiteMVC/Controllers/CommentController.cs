using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IArticleService _articleService;

        public CommentController(ICommentService commentService, IArticleService articleService)
        {
            _commentService = commentService;
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            var article = await _articleService.GetArticleByIdAsync(comment.ArticleId);
            if (article == null)
            {
                ModelState.AddModelError("", "The specified article does not exist.");
                return RedirectToAction("Detail", "Article", new { id = comment.ArticleId });
            }

            comment.Article = article;

            ModelState.Remove("Article");

            if (ModelState.IsValid)
            {
                comment.Status = false; 
                var (success, message) = await _commentService.CreateCommentAsync(comment);
                if (success)
                {
                    return RedirectToAction("Detail", "Article", new { id = comment.ArticleId });
                }
                ModelState.AddModelError(string.Empty, message);
            }
            else
            {
                // ModelState hatalarını ViewData'ya ekleyelim
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewData["ModelStateErrors"] = errors;
            }
            return RedirectToAction("Detail", "Article", new { id = comment.ArticleId });
        }
    }
}
