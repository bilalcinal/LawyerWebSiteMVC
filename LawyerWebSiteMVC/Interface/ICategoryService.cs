using LawyerWebSiteMVC.Data;
namespace LawyerWebSiteMVC.Interface;
public interface ICategoryService
{
    Task<(bool, string)> CreateCategoryAsync(Category category);
    Task<(bool, string)> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(int categoryId);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
}