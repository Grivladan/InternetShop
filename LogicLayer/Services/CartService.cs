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
        private readonly string sessionId;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            sessionId = HttpContext.Current.Session.SessionID;
        }

        public void Create(Cart cart)
        {
            _unitOfWork.Carts.Create(cart);
            _unitOfWork.Save(); 
        }

        public void AddToCart(Book book)
        {
            var cart = _unitOfWork.Carts.Query.SingleOrDefault( x => x.SessionId == sessionId && x.Book.Id == book.Id);
            if(cart == null)
            {
                cart = new Cart
                {
                    Book = book,
                    Count = 1,
                    Date = DateTime.Now
                };
                Create(cart);
            }
            else
            {
                cart.Count++;
                Update(cart.Id, cart);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cart> GetAll()
        {
            var carts = _unitOfWork.Carts.GetAll();
            return carts;
        }

        public Cart GetById(int id)
        {
            var cart = _unitOfWork.Carts.GetById(id);
            return cart;
        }

        public int GetTotal()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Cart cart)
        {
            var cartItem = _unitOfWork.Carts.GetById(id);
            if (cartItem == null)
                throw new InvalidOperationException();
            _unitOfWork.Carts.Update(cart);
            _unitOfWork.Save();
        }
    }
}
