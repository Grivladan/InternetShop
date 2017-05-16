using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAllCartItems();
        void AddToCart(int id);
        int GetCartCount();
        int RemoveFromCart(int id);
        void RemoveAll();
        decimal GetTotal();
        void CreateOrder(Order order);
        int UpdateCartCount(int id, int cartCount);

        void Dispose();
    }
}
