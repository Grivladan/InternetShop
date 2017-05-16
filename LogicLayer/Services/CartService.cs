using System;
using System.Collections.Generic;
using DataAccess.Entities;
using DataAccess.Interfaces;
using LogicLayer.Interfaces;
using System.Web;
using System.Linq;

namespace LogicLayer.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly string sessionId;

        static CartService()
        {
            sessionId = HttpContext.Current.Session.SessionID;
        }

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddToCart(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            var cart = _unitOfWork.Carts.Query.SingleOrDefault( x => x.SessionId == sessionId && x.Product.Id == product.Id);
            if(cart == null)
            {
                cart = new Cart
                {
                    SessionId = sessionId,
                    Product = product,
                    Count = 1,
                    Date = DateTime.Now
                };
                _unitOfWork.Carts.Create(cart);
            }
            else
            {
                cart.Count++;
                _unitOfWork.Carts.Update(cart);
            }
            _unitOfWork.Save();
        }

        public void RemoveFromCart(int id)
        {
            _unitOfWork.Carts.Delete(id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<Cart> GetAllCartItems()
        {
            var carts = _unitOfWork.Carts.Query.Where(x => x.SessionId == sessionId).ToList();
            return carts;
        }

        public int GetCartCount()
        {
            var carts = GetAllCartItems();
            return carts.Sum(x => x.Count);
        }

        public void RemoveAll()
        {
            var cartsId = _unitOfWork.Carts.Query.Select(x => x.Id).ToList();
            foreach (var id in cartsId)
                _unitOfWork.Carts.Delete(id);
            _unitOfWork.Save();
        }

        public decimal GetTotal()
        {
            var carts = _unitOfWork.Carts.Query.Where(x => x.SessionId == sessionId);
            decimal? sum = carts.Select(x => x.Count * x.Product.Price).Sum();
            return sum ?? decimal.Zero;
        }

        public void CreateOrder(Order order)
        {
            var cartItems = GetAllCartItems();
            decimal orderTotal = 0;
            foreach (var item in cartItems)
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

            RemoveAll();
        }

        public int UpdateCartCount(int id, int cartCount)
        {
            // Get the cart 
            var cartItem = _unitOfWork.Carts.GetById(id); 

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartCount > 0)
                {
                    cartItem.Count = cartCount;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _unitOfWork.Carts.Delete(id);
                }
                // Save changes 
                _unitOfWork.Save();
            }
            return itemCount;
        }
    }
}
