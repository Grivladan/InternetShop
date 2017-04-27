using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IOrderService
    {
        void Create(Order order);
        void Update(int id, Order order);
        IEnumerable<Order> GetAll();
        Order GetById(int id);

        void Dispose();
    }
}
