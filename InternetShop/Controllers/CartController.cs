using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public ActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction("Index","Home");
        }

        public ActionResult GetCartItems()
        {
            var cartItems = _cartService.GetAllCartItems();
            return View(cartItems);
        }
    }
}