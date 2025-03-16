using AutoMapper;
using ChildrensLeisure.DataAccess.Repositories;
using ChildrensLeisure.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChildrensLeisureDBContext _context;
        private readonly IMapper _mapper;

        public IOrderRepository Orders { get; }
        public IAttractionRepository Attractions { get; }
        public IEventProgramRepository EventPrograms { get; }
        public IFairyCharacterRepository FairyCharacters { get; }
        public IZoneRepository Zones { get; }

        public UnitOfWork(ChildrensLeisureDBContext context, IMapper mapper,
                    IOrderRepository orders,
                    IAttractionRepository attractions,
                    IEventProgramRepository eventPrograms,
                    IFairyCharacterRepository fairyCharacters,
                    IZoneRepository zones)
        {
            _context = context;
            _mapper = mapper;

            Orders = orders;
            Attractions = attractions;
            EventPrograms = eventPrograms;
            FairyCharacters = fairyCharacters;
            Zones = zones;

        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
