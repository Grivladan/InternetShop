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
        private readonly ICartService _cartService;
        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
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
                _cartService.CreateOrder(order);
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

        public ActionResult ChangeStatus(int id, OrderStatus orderStatus)
        {
            _orderService.ChangeStatus(id, orderStatus);
            return RedirectToAction("Order", "GetAllOrders");
        }

        public ActionResult GetUserOrders()
        {
            return View();
        }
    }
}