using AutoMapper;
using DataAccess.Entities;
using InternetShop.ViewModels;
using LogicLayer.DTO;
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
            var categoriesDto = _categoryService.GetAllCategories();
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDto, CategoryViewModel>());
            var categoriesViewModel = Mapper.Map<IEnumerable<CategoryDto>, IEnumerable<CategoryViewModel>>(categoriesDto);

            return View(categoriesViewModel); 
        }

        public ActionResult GetAllCategoriesAdmin()
        {
            var categoriesDto = _categoryService.GetAllCategories();
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDto, CategoryViewModel>());
            var categoriesViewModel = Mapper.Map<IEnumerable<CategoryDto>, IEnumerable<CategoryViewModel>>(categoriesDto);

            return View(categoriesViewModel);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("GetAllCategoriesAdmin", "Category");
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CategoryViewModel, CategoryDto>());
                var categoryDto = Mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
                _categoryService.Create(categoryDto);
                return RedirectToAction("GetAllCategoriesAdmin", "Category");
            }
            return View(categoryViewModel);
        }

        public ActionResult Edit(int id)
        {
            var categoryDto = _categoryService.GetCategoryById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDto, CategoryViewModel>());
            var categoryViewModel = Mapper.Map<CategoryDto, CategoryViewModel>(categoryDto);
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CategoryViewModel, CategoryDto>());
                var categoryDto = Mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
                _categoryService.Update(categoryViewModel.Id, categoryDto);
                return RedirectToAction("GetAllCategoriesAdmin", "Category");
            }
            return View(categoryViewModel);
        }
    }
}