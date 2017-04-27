using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            _unitOfWork.Orders.Create(order);
            return View(order);
        }
    }
}