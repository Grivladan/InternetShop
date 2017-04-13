using DataAccess.Entities;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Create(Book book);
        void Update(int id, Book book);
        void Remove(int id);

        void Dispose();
    }
}
