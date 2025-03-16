using AutoFixture;
using AutoMapper;
using ChildrensLeisure.BLL.Services;
using ChildrensLeisure.DataAccess.Entity;
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
    public class AttractionServiceTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly AttractionService _attractionService;
        private readonly IFixture _fixture;

        public AttractionServiceTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _attractionService = new AttractionService(_unitOfWorkMock);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllAttractionsAsync_ReturnsAttractions()
        {
            var attractions = _fixture.CreateMany<AttractionModel>(3);
            _unitOfWorkMock.Attractions.GetAllAsync().Returns(attractions);

            var result = await _attractionService.GetAllAttractionsAsync();
            var resultList = result.ToList();

            Assert.NotNull(result);
            Assert.Equal(3, resultList.Count);
            await _unitOfWorkMock.Attractions.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task GetAttractionByIdAsync_ExistingId_ReturnsAttraction()
        {
            var attractionId = Guid.NewGuid();
            var attraction = _fixture.Create<AttractionModel>();
            _unitOfWorkMock.Attractions.GetByIdAsync(attractionId).Returns(attraction);

            var result = await _attractionService.GetAttractionByIdAsync(attractionId);

            Assert.NotNull(result);
            Assert.Equal(attraction, result);
            await _unitOfWorkMock.Attractions.Received(1).GetByIdAsync(attractionId);
        }

        [Fact]
        public async Task CreateAttractionAsync_CallsAddAsyncAndSave()
        {
            var attractionModel = _fixture.Create<AttractionModel>();

            await _attractionService.CreateAttractionAsync(attractionModel);

            await _unitOfWorkMock.Attractions.Received(1).AddAsync(attractionModel);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteAttractionAsync_ExistingId_ReturnsTrue()
        {
            var attractionId = Guid.NewGuid();
            var attraction = _fixture.Create<AttractionModel>();
            _unitOfWorkMock.Attractions.GetByIdAsync(attractionId).Returns(attraction);

            var result = await _attractionService.DeleteAttractionAsync(attractionId);

            Assert.True(result);
            await _unitOfWorkMock.Attractions.Received(1).DeleteAsync(attraction);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteAttractionAsync_NonExistingId_ReturnsFalse()
        {
            var attractionId = Guid.NewGuid();
            _unitOfWorkMock.Attractions.GetByIdAsync(attractionId).Returns((AttractionModel)null);

            var result = await _attractionService.DeleteAttractionAsync(attractionId);

            Assert.False(result);
            await _unitOfWorkMock.Attractions.DidNotReceive().DeleteAsync(Arg.Any<AttractionModel>());
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }

        [Fact]
        public async Task UpdateAttractionAsync_ExistingAttraction_ReturnsTrue()
        {
            var attractionModel = _fixture.Create<AttractionModel>();
            var attraction = _fixture.Create<AttractionModel>();
            _unitOfWorkMock.Attractions.GetByIdAsync(attractionModel.Id).Returns(attraction);

            var result = await _attractionService.UpdateAttractionAsync(attractionModel);

            Assert.True(result);
            await _unitOfWorkMock.Attractions.Received(1).UpdateAsync(attractionModel);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task UpdateAttractionAsync_NonExistingAttraction_ReturnsFalse()
        {
            var attractionModel = _fixture.Create<AttractionModel>();
            _unitOfWorkMock.Attractions.GetByIdAsync(attractionModel.Id).Returns((AttractionModel)null);

            var result = await _attractionService.UpdateAttractionAsync(attractionModel);

            Assert.False(result);
            await _unitOfWorkMock.Attractions.DidNotReceive().UpdateAsync(attractionModel);
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }
    }
}
