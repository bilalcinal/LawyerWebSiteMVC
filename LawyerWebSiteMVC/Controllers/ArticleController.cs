using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var articles = (await _articleService.GetAllArticlesAsync()).ToList();
            if (categoryId.HasValue)
            {
                articles = articles.Where(a => a.CategoryId == categoryId.Value).ToList();
            }

            var categories = (await _categoryService.GetAllCategoriesAsync()).ToList();

            var viewModel = new ArticleViewModel
            {
                Articles = articles,
                Categories = categories
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var viewModel = await _articleService.GetArticleDetailsAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
        
    }

}
