using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IAttractionRepository Attractions { get; }
        IEventProgramRepository EventPrograms { get; }
        IFairyCharacterRepository FairyCharacters { get; }
        IZoneRepository Zones { get; }

        Task<int> SaveAsync();
    }
}
