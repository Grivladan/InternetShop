using InternetShop.DataAccess.Entities;
using LogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        readonly private IProductService _productService;
        readonly private IUserService _userService;
        public AdminController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        public ActionResult GetAllProductsAdmin()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return View(users);
        }

        public ActionResult AddToBlackList(string id)
        {
            _userService.AddToBlackList(id);
            return RedirectToAction("GetAllUsers", "Admin");
        }

        public ActionResult RemoveFromBlackList(string id)
        {
            _userService.RemoveFromBlackList(id);
            return RedirectToAction("GetBlackList","Admin");
        }

        public ActionResult GetBlackList()
        {
            var bannedUsers = _userService.GetBlackList();
            return View(bannedUsers);
        }
    }
}