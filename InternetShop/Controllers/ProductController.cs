using AutoMapper;
using InternetShop.ViewModels;
using LogicLayer.DTO;
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
            var productsDto = _productService.GetAll();

            Mapper.Initialize(cfg => cfg.CreateMap<ProductDto, ProductViewModel>());
            var productsViewModel = Mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductViewModel>>(productsDto);
            
            return View(productsViewModel);
        }

        public ActionResult CreateProduct()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct([Bind(Exclude = "Image")]ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
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
                    productViewModel.Image = imageData;
                }

                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductDto>());
                var productDto = Mapper.Map<ProductViewModel, ProductDto>(productViewModel);

                _productService.Create(productDto);
            }
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(productViewModel);
        }

        public FileContentResult ProductImage(int id)
        {
            var productDto = _productService.GetById(id);
            return new FileContentResult(productDto.Image, "image / jpeg");
        }

        public ActionResult Search(string searchString)
        {
            var productsDto = _productService.Search(searchString);

            Mapper.Initialize(cfg => cfg.CreateMap<ProductDto, ProductViewModel>());
            var productsViewModel = Mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductViewModel>>(productsDto);
            return View("GetProducts", productsViewModel);
        }

        public ActionResult AutocompleteSearch(string searchString)
        {
            var products = _productService.Search(searchString);

            return Json(products.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SortProduct(string sortOrder)
        {
            var productsDto = _productService.Sort(sortOrder);

            Mapper.Initialize(cfg => cfg.CreateMap<ProductDto, ProductViewModel>());
            var productsViewModel = Mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductViewModel>>(productsDto);

            return PartialView("GetProducts", productsViewModel);
        }

        public ActionResult GetProductsByCategory(int categoryId)
        {
            var productsDto = _productService.GetProductsByCategory(categoryId);

            Mapper.Initialize(cfg => cfg.CreateMap<ProductDto, ProductViewModel>());
            var productsViewModel = Mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductViewModel>>(productsDto);

            return PartialView("GetProducts", productsViewModel.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _productService.Remove(id);
            return RedirectToAction("GetAllProductsAdmin", "Admin");
        }

        public ActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if(product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductDto>());
                var productDto = Mapper.Map<ProductViewModel, ProductDto>(productViewModel);
                _productService.Update(productViewModel.Id, productDto);
                return RedirectToAction("GetAllProductsAdmin", "Admin");
            }

            return View(productViewModel);
        }

    }
}