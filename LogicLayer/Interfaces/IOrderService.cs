using DataAccess.Entities;
using LogicLayer.DTO;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IOrderService
    {
        void Create(OrderDto orderDto);
        void Update(int id, OrderDto orderDto);
        void ChangeStatus(int id, OrderStatus orderStatus);
        IEnumerable<OrderDto> GetAll();
        IEnumerable<OrderDto> GetUserOrders(string userId);
        OrderDto GetById(int id);

        void Dispose();
    }
}
