using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.BLL.Services
{
    public class ZoneService : IZoneService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ZoneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ZoneModel>> GetAllZonesAsync()
        {
            return await _unitOfWork.Zones.GetAllAsync();
        }

        public async Task<ZoneModel> GetZoneByIdAsync(Guid id)
        {
            return await _unitOfWork.Zones.GetByIdAsync(id);
        }

        public async Task CreateZoneAsync(ZoneModel model)
        {
            await _unitOfWork.Zones.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteZoneAsync(Guid id)
        {
            var zone = await _unitOfWork.Zones.GetByIdAsync(id);
            if (zone == null) return false;

            await _unitOfWork.Zones.DeleteAsync(zone);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateZoneAsync(ZoneModel model)
        {
            var existingZone = await _unitOfWork.Zones.GetByIdAsync(model.Id);
            if (existingZone == null) return false;

            await _unitOfWork.Zones.UpdateAsync(model);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
