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
using System.Linq;

namespace InternetShop.Tests
{
    public class OrderServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IOrderService _orderService;
        private Fixture _fixture;

        [SetUp]
        public void TestInitialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _orderService = new OrderService(_mockUnitOfWork.Object);
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void GetAllOrders_ListOfOrdersReturned()
        {
            var mockOrders = _fixture.CreateMany<Order>(10);
            _mockUnitOfWork.Setup(u => u.Orders.GetAll()).Returns(mockOrders);

            var orders = _orderService.GetAll();

            orders.ShouldBeEquivalentTo(mockOrders);
        }

        [Test]
        public void GetOrderByValidId_ReturnedOrder()
        {
            var orderId = _fixture.Create<int>();
            var mockOrder = _fixture.Build<Order>().With(x => x.Id, orderId).Create();
            _mockUnitOfWork.Setup(u => u.Orders.GetById(orderId)).Returns(mockOrder);

            var order = _orderService.GetById(orderId);

            order.Should().NotBeNull();
            order.Should().BeOfType<OrderDto>();
            order.Id.Should().Be(orderId);
        }

        [Test]
        public void GetOrderWithInvalidId_ExceptionThrows()
        {
            var orderId = _fixture.Create<int>();
            _mockUnitOfWork.Setup(u => u.Orders.GetById(orderId)).Returns((Order)null);

            Action act = () => _orderService.GetById(orderId);

            act.ShouldThrow<ValidationException>().WithMessage("Order doesn't exist");
        }

        [Test]
        public void ChangeStatusOrderWithValidId_ExpectedStatusChanged()
        {
            var orderId = _fixture.Create<int>();
            var orderStatus = _fixture.Create<OrderStatus>();
            var mockOrder = _fixture.Build<Order>().With(x => x.Id).Create();

            _mockUnitOfWork.Setup(u => u.Orders.GetById(orderId)).Returns(mockOrder);
            _mockUnitOfWork.Setup(u => u.Orders.Update(mockOrder));
            _mockUnitOfWork.Setup(u => u.Save());

           var order = _orderService.ChangeStatus(orderId, orderStatus);

            order.OrderStatus.Should().Be(orderStatus);
            _mockUnitOfWork.Verify(u => u.Orders.Update(mockOrder));
            _mockUnitOfWork.Verify(u => u.Save());
        }

        [Test]
        public void ChangeStatusOrderWithNotExistingId_ExceptionThrows()
        {
            var orderId = _fixture.Create<int>();
            var orderStatus = _fixture.Create<OrderStatus>();
            var mockOrder = _fixture.Build<Order>().With(x => x.Id).Create();

            _mockUnitOfWork.Setup(u => u.Orders.GetById(orderId)).Returns((Order)null);
            _mockUnitOfWork.Setup(u => u.Orders.Update(mockOrder));
            _mockUnitOfWork.Setup(u => u.Save());

            Action act = () => _orderService.ChangeStatus(orderId, orderStatus);
            act.ShouldThrow<ValidationException>().WithMessage("Order doesn't exist");
        }

        [Test]
        public void GetOrdersListOfRequestedUser_OrderListReturned()
        {
            var userId = _fixture.Create<string>();
            var mockOrders = _fixture.CreateMany<Order>(50);
            mockOrders.Skip(10).Take(10).ToList().ForEach(x => x.OwnerId = userId);
            _mockUnitOfWork.Setup(u => u.Orders.Query).Returns(mockOrders.AsQueryable());

            var orders = _orderService.GetUserOrders(userId);

            orders.Should().AllBeOfType<OrderDto>();
            orders.ShouldBeEquivalentTo(mockOrders.Where(x => x.OwnerId == userId));
        }
    }
}
