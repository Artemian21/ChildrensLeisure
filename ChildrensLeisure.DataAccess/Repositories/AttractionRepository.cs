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
    public class AttractionRepository : IAttractionRepository
    {
        private readonly ChildrensLeisureDBContext _context;
        private readonly IMapper _mapper;

        public AttractionRepository(ChildrensLeisureDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(AttractionModel entity)
        {
            var attraction = _mapper.Map<Attraction>(entity);
            await _context.Attractions.AddAsync(attraction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AttractionModel entity)
        {
            var attraction = _mapper.Map<Attraction>(entity);
            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AttractionModel entity)
        {
            var attraction = _mapper.Map<Attraction>(entity);
            _context.Attractions.Update(attraction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AttractionModel>> GetAllAsync()
        {
            var attractions = await _context.Attractions.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<AttractionModel>>(attractions);
        }

        public async Task<AttractionModel> GetByIdAsync(Guid id)
        {
            var attraction = await _context.Attractions.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<AttractionModel>(attraction);
        }
    }
}
