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
            _unitOfWork.Products.Create(product);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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

        public IEnumerable<Product> OrderByName()
        {
            var products = _unitOfWork.Products.Query.OrderBy(x => x.Name);
            return products;
        }

        public IEnumerable<Product> OrderByNameDescending()
        {
            var products = _unitOfWork.Products.Query.OrderByDescending(x => x.Name);
            return products;
        }

        public IEnumerable<Product> OrderByPrice()
        {
            var products = _unitOfWork.Products.Query.OrderBy(x => x.Price);
            return products;
        }

        public IEnumerable<Product> OrderByDate()
        {
            var products = _unitOfWork.Products.Query.OrderBy( x => x.Date);
            return products;
        }
    }
}
