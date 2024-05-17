
using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LawyerWebSiteMVC.Areas.Cms.Controllers
{
    [Route("[controller]")]
    [Area("Cms")]
     public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(category);
        }

        // public async Task<IActionResult> Edit(int id)
        // {
        //     var category = await _categoryService.GetCategoryByIdAsync(id);
        //     if (category == null)
        //         return NotFound();
        //     return View(category);
        // }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var result = await _categoryService.UpdateCategoryAsync(category);
            if (result.Item1)
                return RedirectToAction("Index");
            return View(category);
        }

        // public async Task<IActionResult> Delete(int id)
        // {
        //     var category = await _categoryService.GetCategoryByIdAsync(id);
        //     if (category == null)
        //         return NotFound();
        //     return View(category);
        // }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result)
                return RedirectToAction("Index");
            return View();
        }
    }
}