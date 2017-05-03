using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class AdminController : Controller
    {
        readonly private IProductService _productService;
        readonly private ApplicationUserManager _userManager;
        public AdminController(IProductService productService, ApplicationUserManager userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        public ActionResult GetAllProductsAdmin()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        public ActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}