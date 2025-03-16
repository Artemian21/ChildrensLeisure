using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.BLL.Services
{
    public class FairyCharacterService : IFairyCharacterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FairyCharacterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FairyCharacterModel>> GetAllFairyCharactersAsync()
        {
            return await _unitOfWork.FairyCharacters.GetAllAsync();
        }

        public async Task<FairyCharacterModel> GetFairyCharacterByIdAsync(Guid id)
        {
            return await _unitOfWork.FairyCharacters.GetByIdAsync(id);
        }

        public async Task CreateFairyCharacterAsync(FairyCharacterModel model)
        {
            await _unitOfWork.FairyCharacters.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteFairyCharacterAsync(Guid id)
        {
            var fairyCharacters = await _unitOfWork.FairyCharacters.GetByIdAsync(id);
            if (fairyCharacters == null) return false;

            await _unitOfWork.FairyCharacters.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task UpdateFairyCharacterAsync(FairyCharacterModel model)
        {
            await _unitOfWork.FairyCharacters.UpdateAsync(model);
            await _unitOfWork.SaveAsync();
        }
    }
}
