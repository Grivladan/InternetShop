using AutoMapper;
using InternetShop.ViewModels;
using LogicLayer.DTO;
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
        readonly private IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAll().Where(x => x.IsEnabled == true);
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