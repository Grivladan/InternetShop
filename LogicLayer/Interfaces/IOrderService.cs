using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IOrderService
    {
        void Create(Order order);
        void Update(int id, Order order);
        void ChangeStatus(int id, OrderStatus orderStatus);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetUserOrders(string userId);
        Order GetById(int id);

        void Dispose();
    }
}
