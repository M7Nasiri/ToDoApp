using App.Domain.Core.CategoryAgg.Entities;

namespace App.Domain.Core.CategoryAgg.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category? GetCategory(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(int id, Category category);
        bool DeleteCategory(int id);
    }
}
