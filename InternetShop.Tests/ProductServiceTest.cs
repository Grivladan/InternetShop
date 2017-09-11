using AutoMapper;
using DataAccess.Entities;
using DataAccess.Interfaces;
using FluentAssertions;
using LogicLayer.DTO;
using LogicLayer.Infrastructure;
using LogicLayer.Interfaces;
using LogicLayer.Services;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetShop.Tests
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IProductService _productService;
        private Fixture _fixture;
        private IMapper _mapper;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _productService = new ProductService(_mockUnitOfWork.Object);
            _fixture = new Fixture();
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<TestProfile>();
            });
            _mapper = new Mapper(config);
        }

        [Test]
        public void GetAllProducts_ExpectAllProductsReturned()
        {
            var mockProducts = _fixture.Build<Product>().Without(prop => prop.Category).Create<IEnumerable<Product>>();
            _mockUnitOfWork.Setup(u => u.Products.GetAll()).Returns(mockProducts);

            var products = _productService.GetAll();

            products.Should().NotBeNull();
            products.ShouldBeEquivalentTo(mockProducts);
        }

        [Test]
        public void GetProductByKey_ExpectedProductReturned()
        {
            var id = _fixture.Create<int>();
            _fixture.Customize<Product>(c => c.With(p => p.Id, id));
            var mockProduct  = _fixture.Build<Product>().Without(prop => prop.Category).Create();

            _mockUnitOfWork.Setup(u => u.Products.GetById(id)).Returns(mockProduct);

            var product = _productService.GetById(id);

            product.ShouldBeEquivalentTo(mockProduct);
        }

        [Test]
        public void CreateProduct_ExpectedProductCreated()
        {
            _mockUnitOfWork.Setup(u => u.Products.Create(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(u => u.Save());
            var product = _fixture.Build<Product>().With(x => x.Category, 
                _fixture.Build<Category>().Without(c => c.Products).Create())
                .Create();

            _productService.Create(_mapper.Map<ProductDto>(product));

            _mockUnitOfWork.Verify(u => u.Products.Create(It.IsAny<Product>()));
            _mockUnitOfWork.Verify(u => u.Save());
        }

        [Test]
        public void Create_NullArgumentPassed_Throws()
        {
            Action act = () => _productService.Create(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void RemoveValidProduct_ExpectedProductRemoved()
        {
            var id = _fixture.Create<int>();
            _fixture.Customize<Product>(c => c.With(p => p.Id, id));
            var productToDelete = _fixture.Build<Product>().Without(p => p.Category).Create();
            _mockUnitOfWork.Setup(u => u.Products.Delete(productToDelete));
            _mockUnitOfWork.Setup(u => u.Products.GetById(id)).Returns(productToDelete);
            _mockUnitOfWork.Setup(u => u.Save());

            _productService.Remove(id);

            _mockUnitOfWork.Verify(u => u.Products.Delete(productToDelete));
            _mockUnitOfWork.Verify(u => u.Save());
        }

        [Test]
        public void RemoveNotExistedProduct_ExceptionThrows()
        {
            var id = _fixture.Create<int>();
            _fixture.Customize<Product>(c => c.With(p => p.Id, id));
            var productToDelete = _fixture.Build<Product>().Without(p => p.Category).Create();
            _mockUnitOfWork.Setup(u => u.Products.Delete(productToDelete));
            _mockUnitOfWork.Setup(u => u.Products.GetById(id)).Returns((Product)null);
            _mockUnitOfWork.Setup(u => u.Save());

            Action act = () => _productService.Remove(id);

            act.ShouldThrow<ValidationException>().WithMessage("Product doesn't exist");
        }

        [Test]
        public void Update_NullArgumentPassed_ExceptionThrows()
        {
            Action act = () => _productService.Update(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void UpdateNotExistedProduct_ExceptionThrows()
        {
            var id = _fixture.Create<int>();
            _fixture.Customize<Product>(c => c.With(p => p.Id, id));
            var productToUpdate = _fixture.Build<Product>().Without(p => p.Category).Create();
            _mockUnitOfWork.Setup(u => u.Products.Update(productToUpdate));
            _mockUnitOfWork.Setup(u => u.Products.GetById(id)).Returns((Product)null);
            _mockUnitOfWork.Setup(u => u.Save());

            Action act = () => _productService.Update(_mapper.Map<ProductDto>(productToUpdate));

            act.ShouldThrow<ValidationException>().WithMessage("Product doesn`t exist");
        }

        [Test]
        public void Sort_PassedAscentByName_ReturnsInAscentByNameOrder()
        {
            var mockProducts = _fixture.Build<Product>().Without(prop => prop.Category).Create<IEnumerable<Product>>();
            _mockUnitOfWork.Setup(u => u.Products.GetAll()).Returns(mockProducts);

            var products = _productService.Sort("Name (A - Z)");

            products.ShouldBeEquivalentTo(mockProducts.OrderBy(p => p.Name));
        }

        [Test]
        public void Sort_PassedDescentByPrice_ReturnsInDescentByNameOrder()
        {
            var mockProducts = _fixture.Build<Product>().Without(prop => prop.Category).Create<IEnumerable<Product>>();
            _mockUnitOfWork.Setup(u => u.Products.GetAll()).Returns(mockProducts);

            var products = _productService.Sort("Price (High - Low)");

            products.ShouldBeEquivalentTo(mockProducts.OrderByDescending(p => p.Price));
        }

        [Test]
        public void GetProductsByCategory_ListOfProductsExpected()
        {
            var categoryId = _fixture.Create<int>();
            var products = _fixture.Build<Product>().Without(prop => prop.Category).CreateMany(50);
            products.Take(10).ToList().ForEach(p => p.CategoryId = categoryId);
            products.Skip(10).ToList().ForEach(p => p.CategoryId = categoryId + 1);
            _mockUnitOfWork.Setup(u => u.Products.Query).Returns(products.AsQueryable());

            var selectedProducts = _productService.GetProductsByCategory(categoryId);

            selectedProducts.ShouldBeEquivalentTo(products.Where(p => p.CategoryId == categoryId));
        }

        [Test]
        public void SearchProductsByName_ListOfProductsExpected()
        {
            var productName = _fixture.Create<string>();
            var products = _fixture.Build<Product>().Without(prop => prop.Category).CreateMany(50);
            products.Skip(10).Take(10).ToList().ForEach(p => p.Name = productName);
            _mockUnitOfWork.Setup(u => u.Products.GetAll()).Returns(products);

            var searchedProducts = _productService.Search(productName);

            searchedProducts.ShouldBeEquivalentTo(products.Where(p => p.Name.Contains(productName)));
        }
    }
}
