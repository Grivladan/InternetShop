using DataAccess.Entities;
using DataAccess.Interfaces;
using LogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
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
                order.OwnerId = User.Identity.GetUserId();
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
            return RedirectToAction("GetAllOrders", "Order");
        }

        public ActionResult GetUserOrders()
        {
            string userId = User.Identity.GetUserId();
            var userOrders = _orderService.GetUserOrders(userId);
            return View(userOrders);
        }
    }
}