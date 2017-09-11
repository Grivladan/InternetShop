using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;
using LogicLayer.DTO;
using AutoMapper;
using LogicLayer.Infrastructure;

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

        public void Update(CategoryDto categoryDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDto, Category>());
            var category = _unitOfWork.Categories.GetById(categoryDto.Id);
            if (category == null)
                throw new ValidationException("Category with such id doesn`t exist", "");
            _unitOfWork.Categories.Update(Mapper.Map<CategoryDto, Category>(categoryDto));
            _unitOfWork.Save();
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            if (category == null)
                throw new ValidationException("Category doesn't exist", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDto>());
            return Mapper.Map<Category, CategoryDto>(category);
        }

        public void Delete(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            if (category == null)
                throw new ValidationException("Category doesn't exist", "");
            _unitOfWork.Categories.Delete(category);
            _unitOfWork.Save();
        }
    }
}
