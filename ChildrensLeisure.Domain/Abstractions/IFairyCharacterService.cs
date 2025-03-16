using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IFairyCharacterService
    {
        Task<IEnumerable<FairyCharacterModel>> GetAllFairyCharactersAsync();
        Task<FairyCharacterModel> GetFairyCharacterByIdAsync(Guid id);
        Task UpdateFairyCharacterAsync(FairyCharacterModel model);
        Task CreateFairyCharacterAsync(FairyCharacterModel model);
        Task<bool> DeleteFairyCharacterAsync(Guid id);
    }
}
