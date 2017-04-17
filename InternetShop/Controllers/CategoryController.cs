using DataAccess.Entities;
using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService; 
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories); 
        }

        public ActionResult GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Create(category);
            }
            return View(category);
        }
    }
}