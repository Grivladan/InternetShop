using System;
using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;

namespace LogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Category category)
        {
            _unitOfWork.Categories.Create(category);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            Category category = _unitOfWork.Categories.GetById(id);
            return category;
        }

        public void Update(int id, Category category)
        {
            _unitOfWork.Categories.Update(category);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Categories.Delete(id);
            _unitOfWork.Save();
        }
    }
}
