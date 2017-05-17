using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;
using System.Linq;
using System.Web;
using LogicLayer.DTO;
using AutoMapper;
using System;
using LogicLayer.Infrastructure;

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

        public void ChangeStatus(int id, OrderStatus orderStatus)
        {
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null)
                throw new ValidationException("Order doesn't exist", "");
            order.OrderStatus = orderStatus;
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }

        public IEnumerable<OrderDto> GetUserOrders(string userId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDto>());
            var orders = _unitOfWork.Orders.Query.Where(x => x.OwnerId == userId);
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }
    }
}
