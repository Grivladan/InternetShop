using DataAccess.Entities;
using InternetShop.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using System;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }

        void Save();
    }
}
