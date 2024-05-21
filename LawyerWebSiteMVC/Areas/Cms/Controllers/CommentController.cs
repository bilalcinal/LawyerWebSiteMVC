using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Route("Cms/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IArticleService _articleService;

        public CommentController(ICommentService commentService, IArticleService articleService)
        {
            _commentService = commentService;
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return View(comments);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            ViewBag.Articles = new SelectList(articles, "Id", "Title");
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                var articles = await _articleService.GetAllArticlesAsync();
                ViewBag.Articles = new SelectList(articles, "Id", "Title");
                return View(comment);
            }

            var result = await _commentService.CreateCommentAsync(comment);
            if (result.Item1)
                return RedirectToAction("Index");

            var allArticles = await _articleService.GetAllArticlesAsync();
            ViewBag.Articles = new SelectList(allArticles, "Id", "Title");
            return View(comment);
        }

        [HttpPost]
        [Route("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, bool status)
        {
            var result = await _commentService.UpdateCommentStatusAsync(id, status);
            if (result.Item1)
                return RedirectToAction("Index");
            return View("Error");
        }
    }
}
