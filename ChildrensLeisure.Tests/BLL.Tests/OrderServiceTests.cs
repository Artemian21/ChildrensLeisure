using AutoFixture;
using ChildrensLeisure.BLL.Services;
using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Tests.BLL.Tests
{
    public class OrderServiceTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IAttractionService _attractionService;
        private readonly IFairyCharacterService _fairyCharacterService;
        private readonly IOrderService _orderService;
        private readonly IFixture _fixture;

        public OrderServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                      .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _attractionService = Substitute.For<IAttractionService>();
            _fairyCharacterService = Substitute.For<IFairyCharacterService>();
            _orderService = new OrderService(_unitOfWorkMock, _attractionService, _fairyCharacterService);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ReturnsOrders()
        {
            var orders = _fixture.CreateMany<OrderModel>(3).ToList();
            _unitOfWorkMock.Orders.GetAllAsync().Returns(orders);

            var result = await _orderService.GetAllOrdersAsync();
            var resultList = result.ToList();

            Assert.NotNull(resultList);
            Assert.Equal(3, resultList.Count);
            await _unitOfWorkMock.Orders.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task GetOrderByIdAsync_ExistingId_ReturnsOrder()
        {
            var orderId = Guid.NewGuid();
            var order = _fixture.Create<OrderModel>();
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(order);

            var result = await _orderService.GetOrderByIdAsync(orderId);

            Assert.NotNull(result);
            Assert.Equal(order, result);
            await _unitOfWorkMock.Orders.Received(1).GetByIdAsync(orderId);
        }

        [Fact]
        public async Task CreateOrderAsync_CreatesOrder()
        {
            var order = _fixture.Create<OrderModel>();

            await _orderService.CreateOrderAsync(order);

            await _unitOfWorkMock.Orders.Received(1).AddAsync(order);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteOrderAsync_ExistingId_DeletesOrder()
        {
            var orderId = Guid.NewGuid();
            var order = _fixture.Create<OrderModel>();
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(order);

            var result = await _orderService.DeleteOrderAsync(orderId);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).DeleteAsync(order);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteOrderAsync_NonExistingId_ReturnsFalse()
        {
            var orderId = Guid.NewGuid();
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns((OrderModel)null);

            var result = await _orderService.DeleteOrderAsync(orderId);

            Assert.False(result);
            await _unitOfWorkMock.Orders.DidNotReceive().DeleteAsync(Arg.Any<OrderModel>());
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }

        [Fact]
        public async Task UpdateOrderAsync_UpdatesOrder()
        {
            var order = _fixture.Create<OrderModel>();
            _unitOfWorkMock.Orders.GetByIdAsync(order.Id).Returns(order);

            var result = await _orderService.UpdateOrderAsync(order);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).UpdateAsync(order);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task UpdateOrderAsync_NonExistingOrder_ReturnsFalse()
        {
            var order = _fixture.Create<OrderModel>();
            _unitOfWorkMock.Orders.GetByIdAsync(order.Id).Returns((OrderModel)null);

            var result = await _orderService.UpdateOrderAsync(order);

            Assert.False(result);
            await _unitOfWorkMock.Orders.DidNotReceive().UpdateAsync(order);
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }

        [Fact]
        public async Task AddZoneToOrder_AddsZone()
        {
            var orderId = Guid.NewGuid();
            var zoneId = Guid.NewGuid();
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(new OrderModel());
            _unitOfWorkMock.Zones.GetByIdAsync(zoneId).Returns(new ZoneModel());

            var result = await _orderService.AddZoneToOrder(orderId, zoneId);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).UpdateAsync(Arg.Any<OrderModel>());
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task AddAttractionToOrder_AddsAttraction()
        {
            var orderId = Guid.NewGuid();
            var attractionId = Guid.NewGuid();
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(new OrderModel());
            _unitOfWorkMock.Attractions.GetByIdAsync(attractionId).Returns(new AttractionModel());

            var result = await _orderService.AddAttractionToOrder(orderId, attractionId);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).UpdateAsync(Arg.Any<OrderModel>());
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task AddFairyCharacterToOrder_AddsFairyCharacter()
        {
            var orderId = Guid.NewGuid();
            var fairyCharacterId = Guid.NewGuid();
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(new OrderModel());
            _unitOfWorkMock.FairyCharacters.GetByIdAsync(fairyCharacterId).Returns(new FairyCharacterModel());

            var result = await _orderService.AddFairyCharacterToOrder(orderId, fairyCharacterId);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).UpdateAsync(Arg.Any<OrderModel>());
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task RemoveZoneFromOrder_RemovesZone()
        {
            var orderId = Guid.NewGuid();
            var zoneId = Guid.NewGuid();
            var order = new OrderModel { OrderZones = new List<OrderZoneModel> { new OrderZoneModel { ZoneId = zoneId } } };
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(order);

            var result = await _orderService.RemoveZoneFromOrder(orderId, zoneId);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).UpdateAsync(order);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task RemoveAttractionFromOrder_RemovesAttraction()
        {
            var orderId = Guid.NewGuid();
            var attractionId = Guid.NewGuid();
            var order = new OrderModel
            {
                OrderAttractions = new List<OrderAttractionModel>
        {
            new OrderAttractionModel { AttractionId = attractionId, Attraction = new AttractionModel { Id = attractionId } }
        }
            };
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(order);

            var result = await _orderService.RemoveAttractionFromOrder(orderId, attractionId);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).UpdateAsync(order);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task RemoveFairyCharacterFromOrder_RemovesFairyCharacter()
        {
            var orderId = Guid.NewGuid();
            var fairyCharacterId = Guid.NewGuid();
            var order = new OrderModel
            {
                OrderFairyCharacters = new List<OrderFairyCharacterModel>
        {
            new OrderFairyCharacterModel { FairyCharacterId = fairyCharacterId, FairyCharacter = new FairyCharacterModel { Id = fairyCharacterId } }
        }
            };
            _unitOfWorkMock.Orders.GetByIdAsync(orderId).Returns(order);

            var result = await _orderService.RemoveFairyCharacterFromOrder(orderId, fairyCharacterId);

            Assert.True(result);
            await _unitOfWorkMock.Orders.Received(1).UpdateAsync(order);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }
    }
}
