using System;
using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;
using System.Linq;
using AutoMapper;
using LogicLayer.DTO;

namespace LogicLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(ProductDto productDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDto, Product>());
            var product = Mapper.Map<ProductDto, Product>(productDto);
            if (product.Category == null)
                product.Category = _unitOfWork.Categories.GetById(product.CategoryId??0);
            _unitOfWork.Products.Create(product);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<ProductDto> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDto>());
            var products = _unitOfWork.Products.GetAll();
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public ProductDto GetById(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDto>());
            var product = _unitOfWork.Products.GetById(id);
            return Mapper.Map<Product, ProductDto>(product);
        }

        public void Remove(int id)
        {
            _unitOfWork.Products.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(int id, ProductDto productDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDto, Product>());
            var product = Mapper.Map<ProductDto, Product>(productDto);
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
        }

        public IEnumerable<ProductDto> GetProductsByCategory(int categoryId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDto>());
            var products = _unitOfWork.Products.Query.Where(x => x.Category.Id == categoryId);
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products); 
        }

        public IEnumerable<ProductDto> Sort(string sortOrder)
        {
            var products = _unitOfWork.Products.GetAll();
            switch (sortOrder)
            {
                case "Name (A - Z)":
                    products = products.OrderBy(x => x.Name);
                    break;
                case "Name (Z - A)":
                    products = products.OrderByDescending(x => x.Name);
                    break;
                case "Price (Low - High)":
                    products = products.OrderBy(x => x.Price);
                    break;
                case "Price (High - Low)":
                    products = products.OrderByDescending(x => x.Price);
                    break;
                case "Date (Newest - Oldest)":
                    products = products.OrderByDescending(x => x.Date);
                    break;
                case "Date (Oldest - Newest)":
                    products = products.OrderBy(x => x.Date);
                    break;
                default:
                    products = products.OrderByDescending(x => x.Date);
                    break;
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDto>());
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public IEnumerable<ProductDto> Search(string searchString)
        {
            var products = _unitOfWork.Products.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString)).ToList();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDto>());
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }
    }
}
