using DataAccess.Entities;
using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public ActionResult GetBooks()
        {
            var books = _bookService.GetAll();
            return View(books);
        }

        public ActionResult CreateBook(Product book)
        {
            _bookService.Create(book);
            return View(book);
        }
    }
}