using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IZoneRepository
    {
        Task AddAsync(ZoneModel entity);
        Task DeleteAsync(ZoneModel entity);
        Task UpdateAsync(ZoneModel entity);
        Task<IEnumerable<ZoneModel>> GetAllAsync();
        Task<ZoneModel> GetByIdAsync(Guid id);
    }
}
