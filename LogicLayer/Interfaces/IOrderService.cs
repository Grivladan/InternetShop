using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IOrderService
    {
        void Create(Order order);
        void Update(Order order);
        IEnumerable<Order> GetAll();
        Order GetById(Order order);

        void Dispose();
    }
}
