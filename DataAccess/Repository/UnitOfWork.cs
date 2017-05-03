using DataAccess.Entities;
using DataAccess.Interfaces;
using InternetShop.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private bool _isDisposed;
        private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(ApplicationDbContext context, IRepositoryFactory repositoryFactory,
            UserManager<ApplicationUser> manager)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        private UserManager<ApplicationUser> _manager;
        private IRepository<Product> _productRepository;
        private IRepository<Cart> _cartRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Order> _orderRepository;

        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                return _productRepository ?? (_productRepository = _repositoryFactory.CreateRepository<Product>(_context));
            }
        }

        public IRepository<Cart> Carts
        {
            get
            {
                return _cartRepository ?? (_cartRepository = _repositoryFactory.CreateRepository<Cart>(_context));
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return _categoryRepository ?? (_categoryRepository = _repositoryFactory.CreateRepository<Category>(_context));
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                return _orderRepository ?? (_orderRepository = _repositoryFactory.CreateRepository<Order>(_context));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _manager.Dispose();
                }
            }

            _isDisposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
