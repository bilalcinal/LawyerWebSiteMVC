using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Route("Cms/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return View(articles);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ArticleDto articleDto, List<IFormFile> articlePhotos)
        {
            articleDto.ArticlePhotos = articlePhotos;
            var result = await _articleService.CreateArticleAsync(articleDto);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(articleDto);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
                return NotFound();

            var articleDto = new ArticleDto
            {
                Article = article,
                ArticlePhotos = null
            };

            ViewData["ExistingPhotos"] = article.ArticlePhotos.Select(p => new { p.Id, p.Image }).ToList();

            return View(articleDto);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, ArticleDto articleDto, List<IFormFile> articlePhotos)
        {
            articleDto.ArticlePhotos = articlePhotos;
            var result = await _articleService.UpdateArticleAsync(articleDto);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(articleDto);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _articleService.DeleteArticleAsync(id);
            if (result)
                return RedirectToAction("Index");
            return View("Error");
        }
    }
}
