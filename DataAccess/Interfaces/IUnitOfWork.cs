﻿using DataAccess.Entities;
using InternetShop.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using System;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Cart> Carts { get; }
        IRepository<Category> Categories {get; }
        IRepository<Order> Orders { get; }

        void Save();
    }
}
