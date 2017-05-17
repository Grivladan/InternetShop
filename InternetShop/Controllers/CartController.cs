using LogicLayer.Infrastructure;
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
            try
            {
                _cartService.AddToCart(id);
                return RedirectToAction("Index", "Home");
            }
            catch(ValidationException Ex)
            {
                return Content(Ex.Message);
            }
        }

        public ActionResult GetCartItems()
        {
            var cartItems = _cartService.GetAllCartItems();
            return View(cartItems);
        }

        public ActionResult GetDropDownCartItems() 
        {
            var cartItems = _cartService.GetAllCartItems();
            return PartialView(cartItems);
        }

        public ActionResult RemoveFromCart(int id)
        {
            try
            {
                _cartService.RemoveFromCart(id);
                return RedirectToAction("GetCartItems");
            }
            catch (ValidationException Ex)
            {
                return Content(Ex.Message);
            }
        }

        public ActionResult GetCartCount()
        {
            var countCartItems = _cartService.GetCartCount();
            return Json(new { countCartItems}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCartCount(int id, int cartCount)
        {
            int itemCount = _cartService.UpdateCartCount(id, cartCount);

            return Json(itemCount);
        }
    }
}