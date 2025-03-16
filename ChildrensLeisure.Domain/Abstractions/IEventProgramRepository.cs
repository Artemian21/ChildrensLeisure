using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IEventProgramRepository
    {
        Task AddAsync(EventProgramModel entity);
        Task DeleteAsync(EventProgramModel entity);
        Task UpdateAsync(EventProgramModel entity);
        Task<IEnumerable<EventProgramModel>> GetAllAsync();
        Task<EventProgramModel> GetByIdAsync(Guid id);
    }
}
