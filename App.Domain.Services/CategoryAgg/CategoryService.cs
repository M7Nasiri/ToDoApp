using App.Domain.Core.CategoryAgg.Contracts.Repositories;
using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.CategoryAgg.Entities;
using App.Infra.Data.FileStorageService.Contracts;

namespace App.Domain.Services.CategoryAgg
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IFileService _fileService;
        public CategoryService(ICategoryRepository categoryRepo, IFileService fileService)
        {
            _categoryRepo = categoryRepo;
            _fileService = fileService;
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
            var cat = Get(id);
            return _categoryRepo.UpdateCategory(id, model);
        }
        public bool Delete(int id, Category model)
        {
            return _categoryRepo.DeleteCategory(id);
        }
    }
}
