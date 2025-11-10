using App.Domain.Core.CategoryAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.CategoryAgg.Contracts.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category Get(int categoryId);
        bool Create(Category category);
        bool Update(int id, Category category);
        bool Delete(int id, Category model);
    }
}
