﻿using DataAccess.Entities;
using DataAccess.Interfaces;
using InternetShop.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using System;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private bool _isDisposed;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<ApplicationUser> _manager;

        public UnitOfWork(ApplicationDbContext context, IRepositoryFactory repositoryFactory,
            UserManager<ApplicationUser> manager)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
            _manager = manager;
        }

        private IRepository<Book> _bookRepository;
        private IRepository<Cart> _cartRepository;

        public IRepository<Book> Books
        {
            get
            {
                return _bookRepository ?? (_bookRepository = _repositoryFactory.CreateRepository<Book>(_context));
            }
        }

        public IRepository<Cart> Carts
        {
            get
            {
                return _cartRepository ?? (_cartRepository = _repositoryFactory.CreateRepository<Cart>(_context));
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
