using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Create(Product book);
        void Update(int id, Product book);
        void Remove(int id);

        void Dispose();
    }
}
