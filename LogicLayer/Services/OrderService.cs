using System;
using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;
using System.Linq;
using System.Web;

namespace LogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Order order)
        {
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

        public IEnumerable<Order> GetAll()
        {
            var orders = _unitOfWork.Orders.GetAll();
            return orders;
        }

        public Order GetById(int id)
        {
            var order = _unitOfWork.Orders.GetById(id);
            return order;
        }

        public void Update(int id, Order order)
        {
            var orderItem = _unitOfWork.Orders.GetById(id);
            if (orderItem == null)
                throw new ArgumentNullException();
            _unitOfWork.Orders.Update(orderItem);
            _unitOfWork.Save();
        }

        public void ChangeStatus(int id, OrderStatus orderStatus)
        {
            var order = _unitOfWork.Orders.GetById(id);
            order.OrderStatus = orderStatus;
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }
    }
}
