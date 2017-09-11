using AutoMapper;
using DataAccess.Entities;
using DataAccess.Interfaces;
using LogicLayer.DTO;
using LogicLayer.Infrastructure;
using LogicLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<OrderDto> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDto>());
            var orders = _unitOfWork.Orders.GetAll();
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }

        public OrderDto GetById(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDto>());
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null)
                throw new ValidationException("Order doesn't exist", "");
            return Mapper.Map<Order, OrderDto>(order);
        }

        public void Update(OrderDto orderDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, Order>());
            var order = Mapper.Map<OrderDto, Order>(orderDto);

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }

        public Order ChangeStatus(int id, OrderStatus orderStatus)
        {
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null)
                throw new ValidationException("Order doesn't exist", "");
            order.OrderStatus = orderStatus;
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();

            return order;
        }

        public IEnumerable<OrderDto> GetUserOrders(string userId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDto>());
            var orders = _unitOfWork.Orders.Query.Where(x => x.OwnerId == userId).ToList();
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }
    }
}
