using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAll();
        Cart GetById(int id);
        void Create(Cart cart);
        void Update(int id, Cart cart);
        void Remove(int id);
        void RemoveAll();
        int GetTotal();

        void Dispose();
    }
}
