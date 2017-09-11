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

namespace InternetShop.Tests
{
    [TestFixture]
    public class CategoryServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private ICategoryService _categoryService;
        private Fixture _fixture;
        private IMapper _mapper;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new CategoryService(_mockUnitOfWork.Object);
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<TestProfile>();
            });
            _mapper = new Mapper(config);
        }

        [Test]
        public void GetAllCategories_ExpectAllCategoriesReturned()
        {
            var mockCategories = _fixture.CreateMany<Category>();
            _mockUnitOfWork.Setup(u => u.Categories.GetAll()).Returns(mockCategories);

            var categories = _categoryService.GetAllCategories();

            categories.ShouldBeEquivalentTo(mockCategories);
        }

        [Test]
        public void GetCategoryByValidId_ExpectCategoryReturned()
        {
            int categoryId = _fixture.Create<int>();
            _fixture.Customize<Category>(c => c.With(p => p.Id, categoryId));
            var mockCategory = _fixture.Create<Category>();
            _mockUnitOfWork.Setup(u => u.Categories.GetById(categoryId)).Returns(mockCategory);

            var category = _categoryService.GetCategoryById(categoryId);

            category.ShouldBeEquivalentTo(mockCategory);
        }

        [Test]
        public void GetCategoryByNotExistedId_ExceptionThrows()
        {
            int categoryId = _fixture.Create<int>();
            _mockUnitOfWork.Setup(u => u.Categories.GetById(categoryId)).Returns((Category)null);

            Action act = () => _categoryService.GetCategoryById(categoryId);

            act.ShouldThrow<ValidationException>().WithMessage("Category doesn't exist");
        }

        [Test]
        public void DeleteCategoryByValidId_ExpectCategoryDeleted()
        {
            int categoryId = _fixture.Create<int>();
            _fixture.Customize<Category>(c => c.With(p => p.Id, categoryId));
            var mockCategory = _fixture.Create<Category>();
            _mockUnitOfWork.Setup(u => u.Categories.GetById(categoryId)).Returns(mockCategory);
            _mockUnitOfWork.Setup(u => u.Categories.Delete(mockCategory));
            _mockUnitOfWork.Setup(u => u.Save());

            _categoryService.Delete(categoryId);

            _mockUnitOfWork.Verify(u => u.Categories.Delete(mockCategory));
            _mockUnitOfWork.Verify(u => u.Save());
        }

        [Test]
        public void DeleteCategoryByInvalidId_ExceptionThrows()
        {
            int categoryId = _fixture.Create<int>();
            _fixture.Customize<Category>(c => c.With(p => p.Id, categoryId));
            var mockCategory = _fixture.Create<Category>();
            _mockUnitOfWork.Setup(u => u.Categories.GetById(categoryId)).Returns((Category)null);

            Action act = () => _categoryService.Delete(categoryId);

            act.ShouldThrow<ValidationException>();
        }

        [Test]
        public void UpdateCategory_ExpectedCategorySuccesfullyUpdated()
        {
            int categoryId = _fixture.Create<int>();
            var updatedCategory = _fixture.Build<Category>().With(x => x.Id, categoryId).Create();
            var oldCategory = _fixture.Build<Category>().With(x => x.Id, categoryId).Create();
            _mockUnitOfWork.Setup(u => u.Categories.GetById(categoryId)).Returns(oldCategory);
            _mockUnitOfWork.Setup(u => u.Categories.Update(updatedCategory));
            _mockUnitOfWork.Setup(u => u.Save());

            _categoryService.Update(_mapper.Map<CategoryDto>(updatedCategory));

            _mockUnitOfWork.Verify(u => u.Categories.Update(It.IsAny<Category>()));
            _mockUnitOfWork.Verify(u => u.Save());
        }

        [Test]
        public void UpdateCategoryWithNotExistingId_ExceptionThrows()
        {
            int categoryId = _fixture.Create<int>();
            var updatedCategory = _fixture.Build<Category>().With(x => x.Id, categoryId).Create();
            _mockUnitOfWork.Setup(u => u.Categories.GetById(categoryId)).Returns((Category)null);

            Action act = () => _categoryService.Update(_mapper.Map<CategoryDto>(updatedCategory));

            act.ShouldThrow<ValidationException>();
        }
    }
}
