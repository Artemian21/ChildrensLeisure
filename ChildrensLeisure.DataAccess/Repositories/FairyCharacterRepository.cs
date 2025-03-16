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
    public class FairyCharacterRepository : IFairyCharacterRepository
    {
        private readonly ChildrensLeisureDBContext _context;
        private readonly IMapper _mapper;

        public FairyCharacterRepository(ChildrensLeisureDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FairyCharacterModel>> GetAllAsync()
        {
            var characters = await _context.FairyCharacters.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<FairyCharacterModel>>(characters);
        }

        public async Task<FairyCharacterModel> GetByIdAsync(Guid id)
        {
            var character = await _context.FairyCharacters.AsNoTracking().FirstOrDefaultAsync(z => z.Id == id);
            return _mapper.Map<FairyCharacterModel>(character);
        }

        public async Task AddAsync(FairyCharacterModel model)
        {
            var entity = _mapper.Map<FairyCharacter>(model);
            await _context.FairyCharacters.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var character = await _context.FairyCharacters.FindAsync(id);
            if (character == null) return false;

            _context.FairyCharacters.Remove(character);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(FairyCharacterModel model)
        {
            var entity = _mapper.Map<FairyCharacter>(model);
            _context.FairyCharacters.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
