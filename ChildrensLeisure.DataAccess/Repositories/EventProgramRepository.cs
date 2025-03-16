using AutoMapper;
using ChildrensLeisure.DataAccess.Entity;
using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.DataAccess.Repositories
{
    public class EventProgramRepository : IEventProgramRepository
    {
        private readonly ChildrensLeisureDBContext _context;
        private readonly IMapper _mapper;

        public EventProgramRepository(ChildrensLeisureDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(EventProgramModel entity)
        {
            var eventProgram = _mapper.Map<EventProgram>(entity);
            await _context.EventPrograms.AddAsync(eventProgram);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EventProgramModel entity)
        {
            var eventProgram = _mapper.Map<EventProgram>(entity);
            _context.EventPrograms.Remove(eventProgram);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventProgramModel>> GetAllAsync()
        {
            var eventPrograms = await _context.EventPrograms.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<EventProgramModel>>(eventPrograms);
        }

        public async Task<EventProgramModel> GetByIdAsync(Guid id)
        {
            var eventProgram = await _context.EventPrograms.AsNoTracking().FirstOrDefaultAsync(z => z.Id == id);
            return _mapper.Map<EventProgramModel>(eventProgram);
        }

        public async Task UpdateAsync(EventProgramModel entity)
        {
            var eventProgram = _mapper.Map<EventProgram>(entity);
            _context.EventPrograms.Update(eventProgram);
            await _context.SaveChangesAsync();
        }
    }
}
