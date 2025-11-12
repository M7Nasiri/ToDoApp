using App.Domain.Core.CategoryAgg.Contracts.Repositories;
using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.CategoryAgg.Entities;

namespace App.Domain.Services.CategoryAgg
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public bool Create(Category model)
        {
            model.CreatedAt = DateTime.Now;
            return _categoryRepo.CreateCategory(model);
        }

        public List<Category> GetAll()
        {
            return _categoryRepo.GetAllCategories();
        }

        public Category? Get(int categoryId)
        {
            return _categoryRepo.GetCategory(categoryId);
        }

        public bool Update(int id, Category model)
        {
            //var cat = Get(id);
            return _categoryRepo.UpdateCategory(id, model);
        }
        public bool Delete(int id, Category model)
        {
            return _categoryRepo.DeleteCategory(id);
        }
    }
}
