using DataAccess.Entities;
using LogicLayer.DTO;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAllCartItems();
        void AddToCart(int id);
        int GetCartCount();
        void RemoveFromCart(int id);
        void RemoveAll();
        decimal GetTotal();
        void CreateOrder(OrderDto orderDto);
        int UpdateCartCount(int id, int cartCount);

        void Dispose();
    }
}
