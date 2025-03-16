using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IAttractionService
    {
        Task<IEnumerable<AttractionModel>> GetAllAttractionsAsync();
        Task<AttractionModel> GetAttractionByIdAsync(Guid id);
        Task CreateAttractionAsync(AttractionModel model);
        Task<bool> DeleteAttractionAsync(Guid id);
        Task<bool> UpdateAttractionAsync(AttractionModel model);
    }
}
