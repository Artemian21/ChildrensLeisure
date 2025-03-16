using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IAttractionRepository
    {
        Task AddAsync(AttractionModel entity);
        Task DeleteAsync(AttractionModel entity);
        Task UpdateAsync(AttractionModel entity);
        Task<IEnumerable<AttractionModel>> GetAllAsync();
        Task<AttractionModel> GetByIdAsync(Guid id);
    }
}
