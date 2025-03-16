using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IEventProgramService
    {
        Task<IEnumerable<EventProgramModel>> GetAllEventProgramsAsync();
        Task<EventProgramModel> GetEventProgramByIdAsync(Guid id);
        Task UpdateEventProgramAsync(EventProgramModel model);
        Task CreateEventProgramAsync(EventProgramModel model);
        Task<bool> DeleteEventProgramAsync(Guid id);
    }
}
