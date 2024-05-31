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
            if (ModelState.IsValid)
            {
                var article = await _articleService.GetArticleByIdAsync(comment.ArticleId);
                if (article == null)
                {
                    ModelState.AddModelError("", "The specified article does not exist.");
                    return RedirectToAction("Detail", "Article", new { id = comment.ArticleId });
                }

                comment.Article = article; // Set the Article property
                var (success, message) = await _commentService.CreateCommentAsync(comment);
                if (success)
                {
                    return RedirectToAction("Detail", "Article", new { id = comment.ArticleId });
                }
                ModelState.AddModelError(string.Empty, message);
            }
            return RedirectToAction("Detail", "Article", new { id = comment.ArticleId });
        }
    }
}
