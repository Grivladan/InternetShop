using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Create(Product product);
        void Update(int id, Product product);
        void Remove(int id);
        IEnumerable<Product> Search(string searchString);
        IEnumerable<Product> Sort(string sortOrder);

        void Dispose();
    }
}
