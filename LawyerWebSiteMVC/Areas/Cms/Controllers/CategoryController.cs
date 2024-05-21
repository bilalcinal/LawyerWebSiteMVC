using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Route("Cms/[controller]")]
    [Area("Cms")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(category);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            category.Id = id;
            var result = await _categoryService.UpdateCategoryAsync(category);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(category);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result)
                return RedirectToAction("Index");
            return View();
        }
    }
}
