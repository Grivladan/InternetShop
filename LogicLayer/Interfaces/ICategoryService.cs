using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface ICategoryService
    {
        Category GetCategoryById(int id);
        void Create(Category category);
        void Update(int id, Category category);
        void Delete(int id);
        IEnumerable<Category> GetAllCategories();

        void Dispose();
    }
}
