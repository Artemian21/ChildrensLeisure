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
    public class EventProgramServiceTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IEventProgramService _eventProgramService;
        private readonly IFixture _fixture;

        public EventProgramServiceTests()
        {
            _fixture = new Fixture();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _eventProgramService = new EventProgramService(_unitOfWorkMock);
        }

        [Fact]
        public async Task GetAllEventProgramsAsync_ReturnsEventPrograms()
        {
            var eventPrograms = _fixture.CreateMany<EventProgramModel>(3).ToList();
            _unitOfWorkMock.EventPrograms.GetAllAsync().Returns(eventPrograms);

            var result = await _eventProgramService.GetAllEventProgramsAsync();
            var resultList = result.ToList();

            Assert.NotNull(resultList);
            Assert.Equal(3, resultList.Count);
            await _unitOfWorkMock.EventPrograms.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task GetEventProgramByIdAsync_ExistingId_ReturnsEventProgram()
        {
            var eventProgramId = Guid.NewGuid();
            var eventProgram = _fixture.Create<EventProgramModel>();
            _unitOfWorkMock.EventPrograms.GetByIdAsync(eventProgramId).Returns(eventProgram);

            var result = await _eventProgramService.GetEventProgramByIdAsync(eventProgramId);

            Assert.NotNull(result);
            Assert.Equal(eventProgram, result);
            await _unitOfWorkMock.EventPrograms.Received(1).GetByIdAsync(eventProgramId);
        }

        [Fact]
        public async Task CreateEventProgramAsync_CreatesEventProgram()
        {
            var eventProgram = _fixture.Create<EventProgramModel>();

            await _eventProgramService.CreateEventProgramAsync(eventProgram);

            await _unitOfWorkMock.EventPrograms.Received(1).AddAsync(eventProgram);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteEventProgramAsync_ExistingId_DeletesEventProgram()
        {
            var eventProgramId = Guid.NewGuid();
            var eventProgram = _fixture.Create<EventProgramModel>();
            _unitOfWorkMock.EventPrograms.GetByIdAsync(eventProgramId).Returns(eventProgram);

            var result = await _eventProgramService.DeleteEventProgramAsync(eventProgramId);

            Assert.True(result);
            await _unitOfWorkMock.EventPrograms.Received(1).DeleteAsync(eventProgram);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteEventProgramAsync_NonExistingId_ReturnsFalse()
        {
            var eventProgramId = Guid.NewGuid();
            _unitOfWorkMock.EventPrograms.GetByIdAsync(eventProgramId).Returns((EventProgramModel)null);

            var result = await _eventProgramService.DeleteEventProgramAsync(eventProgramId);

            Assert.False(result);
            await _unitOfWorkMock.EventPrograms.DidNotReceive().DeleteAsync(Arg.Any<EventProgramModel>());
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }

        [Fact]
        public async Task UpdateEventProgramAsync_UpdatesEventProgram()
        {
            var eventProgram = _fixture.Create<EventProgramModel>();

            await _eventProgramService.UpdateEventProgramAsync(eventProgram);

            await _unitOfWorkMock.EventPrograms.Received(1).UpdateAsync(eventProgram);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }
    }
}
