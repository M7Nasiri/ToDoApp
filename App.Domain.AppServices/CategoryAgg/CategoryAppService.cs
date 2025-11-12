using App.Domain.Core.CategoryAgg.Contracts.AppServices;
using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.CategoryAgg.Entities;

namespace App.Domain.AppServices.CategoryAgg
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;

        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public bool Create(Category category)
        {
            return _categoryService.Create(category);
        }

        public bool Delete(int id, Category model)
        {
            return _categoryService.Delete(id, model);
        }

        public Category Get(int categoryId)
        {
            return _categoryService.Get(categoryId);
        }

        public List<Category> GetAll()
        {
            return _categoryService.GetAll();
        }

        public bool Update(int id, Category category)
        {
            return _categoryService.Update(id, category);
        }
    }
}
