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

namespace ChildrensLeisure.BLL.Services
{
    public class AttractionService : IAttractionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttractionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AttractionModel>> GetAllAttractionsAsync()
        {
            return await _unitOfWork.Attractions.GetAllAsync();
        }

        public async Task<AttractionModel> GetAttractionByIdAsync(Guid id)
        {
            return await _unitOfWork.Attractions.GetByIdAsync(id);
        }

        public async Task CreateAttractionAsync(AttractionModel model)
        {
            await _unitOfWork.Attractions.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteAttractionAsync(Guid id)
        {
            var attraction = await _unitOfWork.Attractions.GetByIdAsync(id);
            if (attraction == null) return false;

            await _unitOfWork.Attractions.DeleteAsync(attraction);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateAttractionAsync(AttractionModel model)
        {
            var attraction = await _unitOfWork.Attractions.GetByIdAsync(model.Id);
            if (attraction == null) return false;

            await _unitOfWork.Attractions.UpdateAsync(model);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
