using System;
using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;
using LogicLayer.DTO;
using AutoMapper;

namespace LogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(CategoryDto categoryDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDto, Category>());
            var category = Mapper.Map<CategoryDto, Category>(categoryDto);
            _unitOfWork.Categories.Create(category);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDto>());
            var categories = _unitOfWork.Categories.GetAll();
            return Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
        }

        public void Update(int id, CategoryDto categoryDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDto, Category>());
            var category = Mapper.Map<CategoryDto, Category>(categoryDto);
            _unitOfWork.Categories.Update(category);
            _unitOfWork.Save();
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDto>());
            return Mapper.Map<Category, CategoryDto>(category);
        }

        public void Delete(int id)
        {
            _unitOfWork.Categories.Delete(id);
            _unitOfWork.Save();
        }
    }
}
