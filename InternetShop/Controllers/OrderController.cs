using DataAccess.Entities;
using DataAccess.Interfaces;
using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderService.Create(order);
                return RedirectToAction("Index","Home");
            }
            return View(order);
        }

        public ActionResult GetAllOrders()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        public ActionResult GetOrderById(int id)
        {
            var order = _orderService.GetById(id);
            return View(order);
        }
    }
}