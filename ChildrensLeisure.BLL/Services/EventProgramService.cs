using ChildrensLeisure.DataAccess.Repositories;
using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using ChildrensLeisure.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.BLL.Services
{
    public class EventProgramService : IEventProgramService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventProgramService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EventProgramModel>> GetAllEventProgramsAsync()
        {
            return await _unitOfWork.EventPrograms.GetAllAsync();
        }

        public async Task<EventProgramModel> GetEventProgramByIdAsync(Guid id)
        {
            return await _unitOfWork.EventPrograms.GetByIdAsync(id);
        }

        public async Task CreateEventProgramAsync(EventProgramModel model)
        {
            await _unitOfWork.EventPrograms.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteEventProgramAsync(Guid id)
        {
            var eventProgram = await _unitOfWork.EventPrograms.GetByIdAsync(id);
            if (eventProgram == null) return false;

            await _unitOfWork.EventPrograms.DeleteAsync(eventProgram);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task UpdateEventProgramAsync(EventProgramModel model)
        {
            await _unitOfWork.EventPrograms.UpdateAsync(model);
            await _unitOfWork.SaveAsync();
        }
    }
}
