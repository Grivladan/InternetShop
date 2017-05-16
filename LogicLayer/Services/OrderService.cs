using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;
using System.Linq;
using System.Web;
using LogicLayer.DTO;
using AutoMapper;

namespace LogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(OrderDto orderDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, Order>());
            var order = Mapper.Map<OrderDto, Order>(orderDto);

            var cartItems = _unitOfWork.Carts.Query.Where(x => x.SessionId == HttpContext.Current.Session.SessionID);
            decimal orderTotal = 0;
            foreach(var item in cartItems)
            {
                var orderDetail = new Detail
                {
                    Order = order,
                    Product = item.Product,
                    ProductId = item.Product.Id,
                    Quantity = item.Count,
                    UnitPrice = item.Product.Price
                };

                _unitOfWork.Details.Create(orderDetail);
                orderTotal += item.Count * item.Product.Price;
                
            }
            order.Total = orderTotal;

            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();
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
            return Mapper.Map<Order, OrderDto>(order);
        }

        public void Update(int id, OrderDto orderDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, Order>());
            var order = Mapper.Map<OrderDto, Order>(orderDto);

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }

        public void ChangeStatus(int id, OrderStatus orderStatus)
        {
            var order = _unitOfWork.Orders.GetById(id);
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
