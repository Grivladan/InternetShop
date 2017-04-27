using System;
using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;

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
    }
}
