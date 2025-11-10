using App.Domain.Core.CategoryAgg.Contracts.Repositories;
using App.Domain.Core.CategoryAgg.Entities;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repo.Ef.CategoryAgg
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return _context.SaveChanges() > 0;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category? GetCategory(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }


        public bool UpdateCategory(int id, Category model)
        {
            var cat = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (cat != null)
            {
                cat.Title = model.Title;
                cat.Description = model.Description;
            }
            return _context.SaveChanges() > 0;
        }
        public bool DeleteCategory(int id)
        {
            return _context.Categories.Where(u => u.Id == id).ExecuteDelete() > 0;
        }
    }
}
