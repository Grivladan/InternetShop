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
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult GetProducts()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        public ActionResult CreateProduct(Product product)
        {
            _productService.Create(product);
            return View(product);
        }
    }
}