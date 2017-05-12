using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAll();
        Cart GetById(int id);
        void Create(Cart cart);
        void AddToCart(int id);
        IEnumerable<Cart> GetAllCartItems();
        int GetCartCount();
        void Update(int id, Cart cart);
        void Remove(int id);
        int RemoveFromCart(int id);
        void RemoveAll();
        decimal GetTotal();
        void CreateOrder(Order order);

        void Dispose();
    }
}
