using LogicLayer.DTO;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
        void Create(ProductDto productDto);
        void Update(ProductDto productDto);
        void Remove(int id);
        IEnumerable<ProductDto> Search(string searchString);
        IEnumerable<ProductDto> Sort(string sortOrder);
        IEnumerable<ProductDto> GetProductsByCategory(int categoryId);

        void Dispose();
    }
}
