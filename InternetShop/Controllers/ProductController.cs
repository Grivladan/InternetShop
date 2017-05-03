﻿using DataAccess.Entities;
using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userService = userService;
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
        public ActionResult CreateProduct([Bind(Exclude = "Image")]Product product)
        {
            byte[] imageData = null;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["Image"];
                if (poImgFile != null && poImgFile.ContentLength > 0)
                {
                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }
            }

            if (imageData != null)
            {
                product.Image = imageData;
            }
            _productService.Create(product);
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        public FileContentResult ProductImage(int id)
        {
            var product = _productService.GetById(id);
            return new FileContentResult(product.Image, "image / jpeg");
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

        public ActionResult SortProduct(string sortOrder)
        {
            var products = _productService.Sort(sortOrder);
            return PartialView("GetProducts", products);
        }

        public ActionResult GetProductsByCategory(int categoryId = 0)
        {
            var products = _productService.GetProductsByCategory(categoryId);
            return PartialView("GetProducts", products.ToList());
        }

        public ActionResult Delete(int id)
        {
            _productService.Remove(id);
            return RedirectToAction("GetAllProductsAdmin", "Admin");
        }

    }
}