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
    public class ZoneRepository : IZoneRepository
    {
        private readonly ChildrensLeisureDBContext _context;
        private readonly IMapper _mapper;

        public ZoneRepository(ChildrensLeisureDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(ZoneModel entity)
        {
            var zone = _mapper.Map<Zone>(entity);
            await _context.Zones.AddAsync(zone);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ZoneModel entity)
        {
            var zone = _mapper.Map<Zone>(entity);
            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ZoneModel>> GetAllAsync()
        {
            var zones = await _context.Zones.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<ZoneModel>>(zones);
        }

        public async Task<ZoneModel> GetByIdAsync(Guid id)
        {
            var zone = await _context.Zones.AsNoTracking().FirstOrDefaultAsync(z => z.Id == id);
            return _mapper.Map<ZoneModel>(zone);
        }

        public async Task UpdateAsync(ZoneModel entity)
        {
            var zone = _mapper.Map<Zone>(entity);

            var existingZone = await _context.Zones.FindAsync(zone.Id);
            if (existingZone != null)
            {
                _context.Entry(existingZone).CurrentValues.SetValues(zone);
            }
            else
            {
                _context.Zones.Update(zone);
            }

            await _context.SaveChangesAsync();
        }
    }
}
