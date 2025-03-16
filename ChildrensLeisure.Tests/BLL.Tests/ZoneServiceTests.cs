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
    public class ZoneServiceTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IZoneService _zoneService;
        private readonly IFixture _fixture;

        public ZoneServiceTests()
        {
            _fixture = new Fixture();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _zoneService = new ZoneService(_unitOfWorkMock);
        }

        [Fact]
        public async Task GetAllZonesAsync_ReturnsZones()
        {
            var zones = _fixture.CreateMany<ZoneModel>(3).ToList();
            _unitOfWorkMock.Zones.GetAllAsync().Returns(zones);

            var result = await _zoneService.GetAllZonesAsync();
            var resultList = result.ToList();

            Assert.NotNull(resultList);
            Assert.Equal(3, resultList.Count);
            await _unitOfWorkMock.Zones.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task GetZoneByIdAsync_ExistingId_ReturnsZone()
        {
            var zoneId = Guid.NewGuid();
            var zone = _fixture.Create<ZoneModel>();
            _unitOfWorkMock.Zones.GetByIdAsync(zoneId).Returns(zone);

            var result = await _zoneService.GetZoneByIdAsync(zoneId);

            Assert.NotNull(result);
            Assert.Equal(zone, result);
            await _unitOfWorkMock.Zones.Received(1).GetByIdAsync(zoneId);
        }

        [Fact]
        public async Task CreateZoneAsync_CreatesZone()
        {
            var zone = _fixture.Create<ZoneModel>();

            await _zoneService.CreateZoneAsync(zone);

            await _unitOfWorkMock.Zones.Received(1).AddAsync(zone);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteZoneAsync_ExistingId_DeletesZone()
        {
            var zoneId = Guid.NewGuid();
            var zone = _fixture.Create<ZoneModel>();
            _unitOfWorkMock.Zones.GetByIdAsync(zoneId).Returns(zone);

            var result = await _zoneService.DeleteZoneAsync(zoneId);

            Assert.True(result);
            await _unitOfWorkMock.Zones.Received(1).DeleteAsync(zone);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteZoneAsync_NonExistingId_ReturnsFalse()
        {
            var zoneId = Guid.NewGuid();
            _unitOfWorkMock.Zones.GetByIdAsync(zoneId).Returns((ZoneModel)null);

            var result = await _zoneService.DeleteZoneAsync(zoneId);

            Assert.False(result);
            await _unitOfWorkMock.Zones.DidNotReceive().DeleteAsync(Arg.Any<ZoneModel>());
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }

        [Fact]
        public async Task UpdateZoneAsync_UpdatesZone()
        {
            var zone = _fixture.Create<ZoneModel>();
            _unitOfWorkMock.Zones.GetByIdAsync(zone.Id).Returns(zone);

            var result = await _zoneService.UpdateZoneAsync(zone);

            Assert.True(result);
            await _unitOfWorkMock.Zones.Received(1).UpdateAsync(zone);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task UpdateZoneAsync_NonExistingZone_ReturnsFalse()
        {
            var zone = _fixture.Create<ZoneModel>();
            _unitOfWorkMock.Zones.GetByIdAsync(zone.Id).Returns((ZoneModel)null);

            var result = await _zoneService.UpdateZoneAsync(zone);

            Assert.False(result);
            await _unitOfWorkMock.Zones.DidNotReceive().UpdateAsync(zone);
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }
    }
}
