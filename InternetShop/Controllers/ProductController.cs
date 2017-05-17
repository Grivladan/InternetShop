using AutoMapper;
using InternetShop.ViewModels;
using LogicLayer.DTO;
using LogicLayer.Infrastructure;
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

        [Authorize(Roles = "admin")]
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

                if (imageData == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/UtilImages/No_Image.png");

                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            imageData = br.ReadBytes((int)imageFileLength);
                        }
                    }
                }

                productViewModel.Image = imageData;

                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductDto>());
                var productDto = Mapper.Map<ProductViewModel, ProductDto>(productViewModel);

                _productService.Create(productDto);
                return RedirectToAction("GetAllProductsAdmin", "Product");
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
            try
            {
                _productService.Remove(id);
                return RedirectToAction("GetAllProductsAdmin", "Product");
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetAllProductsAdmin()
        {
            var productsDto = _productService.GetAll();

            Mapper.Initialize(cfg => cfg.CreateMap<ProductDto, ProductViewModel>());
            var productsViewModel = Mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductViewModel>>(productsDto);
            return View(productsViewModel);
        }

    }
}