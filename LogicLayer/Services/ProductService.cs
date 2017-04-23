﻿using System;
using System.Collections.Generic;
using DataAccess.Entities;
using LogicLayer.Interfaces;
using DataAccess.Interfaces;
using System.Linq;

namespace LogicLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Product product)
        {
            if (product.Category == null)
                product.Category = _unitOfWork.Categories.GetById(product.CategoryId??0);
            _unitOfWork.Products.Create(product);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _unitOfWork.Products.GetAll();
            return products;
        }

        public Product GetById(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            return product;
        }

        public void Remove(int id)
        {
            _unitOfWork.Products.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(int id, Product product)
        {
            var productItem = _unitOfWork.Products.GetById(id);
            if (productItem == null)
                throw new Exception();
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var products = _unitOfWork.Products.Query.Where(x => x.Category.Id == categoryId);
            return products;
        }

        public IEnumerable<Product> Sort(string sortOrder)
        {
            var products = _unitOfWork.Products.GetAll();
            switch (sortOrder)
            {
                default:
                    products.OrderBy(x => x.Name);
                    break;
            }
            return products;
        }

        public IEnumerable<Product> Search(string searchString)
        {
            var products = _unitOfWork.Products.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString)).ToList();
            }

            return products;
        }
    }
}
