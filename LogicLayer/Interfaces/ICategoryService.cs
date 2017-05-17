using LogicLayer.DTO;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto GetCategoryById(int id);
        void Create(CategoryDto category);
        void Update(CategoryDto category);
        void Delete(int id);
        IEnumerable<CategoryDto> GetAllCategories();

        void Dispose();
    }
}
