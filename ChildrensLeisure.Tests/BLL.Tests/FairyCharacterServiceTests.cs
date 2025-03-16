using AutoFixture;
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
    public class FairyCharacterServiceTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IFairyCharacterService _fairyCharacterService;
        private readonly IFixture _fixture;

        public FairyCharacterServiceTests()
        {
            _fixture = new Fixture();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _fairyCharacterService = new FairyCharacterService(_unitOfWorkMock);
        }

        [Fact]
        public async Task GetAllFairyCharactersAsync_ReturnsFairyCharacters()
        {
            var fairyCharacters = _fixture.CreateMany<FairyCharacterModel>(3).ToList();
            _unitOfWorkMock.FairyCharacters.GetAllAsync().Returns(fairyCharacters);

            var result = await _fairyCharacterService.GetAllFairyCharactersAsync();
            var resultList = result.ToList();

            Assert.NotNull(resultList);
            Assert.Equal(3, resultList.Count);
            await _unitOfWorkMock.FairyCharacters.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task GetFairyCharacterByIdAsync_ExistingId_ReturnsFairyCharacter()
        {
            var fairyCharacterId = Guid.NewGuid();
            var fairyCharacter = _fixture.Create<FairyCharacterModel>();
            _unitOfWorkMock.FairyCharacters.GetByIdAsync(fairyCharacterId).Returns(fairyCharacter);

            var result = await _fairyCharacterService.GetFairyCharacterByIdAsync(fairyCharacterId);

            Assert.NotNull(result);
            Assert.Equal(fairyCharacter, result);
            await _unitOfWorkMock.FairyCharacters.Received(1).GetByIdAsync(fairyCharacterId);
        }

        [Fact]
        public async Task CreateFairyCharacterAsync_CreatesFairyCharacter()
        {
            var fairyCharacter = _fixture.Create<FairyCharacterModel>();

            await _fairyCharacterService.CreateFairyCharacterAsync(fairyCharacter);

            await _unitOfWorkMock.FairyCharacters.Received(1).AddAsync(fairyCharacter);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteFairyCharacterAsync_ExistingId_DeletesFairyCharacter()
        {
            var fairyCharacterId = Guid.NewGuid();
            var fairyCharacter = _fixture.Create<FairyCharacterModel>();
            _unitOfWorkMock.FairyCharacters.GetByIdAsync(fairyCharacterId).Returns(fairyCharacter);

            var result = await _fairyCharacterService.DeleteFairyCharacterAsync(fairyCharacterId);

            Assert.True(result);
            await _unitOfWorkMock.FairyCharacters.Received(1).DeleteAsync(fairyCharacterId);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }

        [Fact]
        public async Task DeleteFairyCharacterAsync_NonExistingId_ReturnsFalse()
        {
            var fairyCharacterId = Guid.NewGuid();
            _unitOfWorkMock.FairyCharacters.GetByIdAsync(fairyCharacterId).Returns((FairyCharacterModel)null);

            var result = await _fairyCharacterService.DeleteFairyCharacterAsync(fairyCharacterId);

            Assert.False(result);
            await _unitOfWorkMock.FairyCharacters.DidNotReceive().DeleteAsync(fairyCharacterId);
            await _unitOfWorkMock.DidNotReceive().SaveAsync();
        }

        [Fact]
        public async Task UpdateFairyCharacterAsync_UpdatesFairyCharacter()
        {
            var fairyCharacter = _fixture.Create<FairyCharacterModel>();

            await _fairyCharacterService.UpdateFairyCharacterAsync(fairyCharacter);

            await _unitOfWorkMock.FairyCharacters.Received(1).UpdateAsync(fairyCharacter);
            await _unitOfWorkMock.Received(1).SaveAsync();
        }
    }
}
