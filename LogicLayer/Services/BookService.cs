using System;
using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;

namespace LogicLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Book Create(Book book)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            var books = _unitOfWork.Books.GetAll();
            return books;
        }

        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
