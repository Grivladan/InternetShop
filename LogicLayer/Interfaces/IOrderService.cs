using DataAccess.Entities;
using LogicLayer.DTO;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IOrderService
    {
        void Update(OrderDto orderDto);
        void ChangeStatus(int id, OrderStatus orderStatus);
        IEnumerable<OrderDto> GetAll();
        IEnumerable<OrderDto> GetUserOrders(string userId);
        OrderDto GetById(int id);

        void Dispose();
    }
}
