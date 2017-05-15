using System;
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
