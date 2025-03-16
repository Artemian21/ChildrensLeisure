using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IZoneService
    {
        Task<IEnumerable<ZoneModel>> GetAllZonesAsync();
        Task<ZoneModel> GetZoneByIdAsync(Guid id);
        Task<bool> UpdateZoneAsync(ZoneModel model);
        Task CreateZoneAsync(ZoneModel model);
        Task<bool> DeleteZoneAsync(Guid id);
    }
}
