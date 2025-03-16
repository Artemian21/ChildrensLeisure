using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IFairyCharacterRepository
    {
        Task<IEnumerable<FairyCharacterModel>> GetAllAsync();
        Task<FairyCharacterModel> GetByIdAsync(Guid id);
        Task AddAsync(FairyCharacterModel model);
        Task UpdateAsync(FairyCharacterModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
