using DataAccess.Entities;
using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult GetProducts()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        public ActionResult CreateProduct()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            _productService.Create(product);
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        public ActionResult Search(string searchString)
        {
            var products = _productService.Search(searchString);
            return View("GetProducts", products);
        }

        public ActionResult AutocompleteSearch(string searchString)
        {
            var products = _productService.Search(searchString);

            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}