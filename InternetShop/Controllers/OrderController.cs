using AutoMapper;
using DataAccess.Entities;
using DataAccess.Interfaces;
using InternetShop.ViewModels;
using LogicLayer.DTO;
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
        public ActionResult CreateOrder(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDto>());
                var orderDto = Mapper.Map<OrderViewModel, OrderDto>(orderViewModel);

                orderDto.OwnerId = User.Identity.GetUserId();
                _cartService.CreateOrder(orderDto);
                return RedirectToAction("Index","Home");
            }
            return View(orderViewModel);
        }

        public ActionResult GetAllOrders()
        {
            var ordersDto = _orderService.GetAll();

            Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, OrderViewModel>());
            var ordersViewModel = Mapper.Map<IEnumerable<OrderDto>,IEnumerable<OrderViewModel>>(ordersDto);
            return View(ordersViewModel);
        }

        public ActionResult GetOrderById(int id)
        {
            try
            {
                var orderDto = _orderService.GetById(id);

                Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, OrderViewModel>());
                var orderViewModel = Mapper.Map<OrderDto, OrderViewModel>(orderDto);
                return View(orderViewModel);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult ChangeStatus(int id, OrderStatus orderStatus)
        {
            try
            {
                _orderService.ChangeStatus(id, orderStatus);
                return RedirectToAction("GetAllOrders", "Order");
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult GetUserOrders()
        {
            string userId = User.Identity.GetUserId();
            var userOrdersDto = _orderService.GetUserOrders(userId);

            Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, OrderViewModel>());
            var userOrdersViewModel = Mapper.Map<IEnumerable<OrderDto>, IEnumerable<OrderViewModel>>(userOrdersDto);
            return View(userOrdersViewModel);
        }
    }
}