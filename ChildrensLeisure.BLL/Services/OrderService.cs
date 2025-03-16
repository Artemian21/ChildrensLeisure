using AutoMapper;
using ChildrensLeisure.DataAccess.Entity;
using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFairyCharacterService _fairyCharacterService;
        private readonly IAttractionService _attractionService;

        public OrderService(IUnitOfWork unitOfWork, IAttractionService attractionService, IFairyCharacterService fairyCharacterService)
        {
            _unitOfWork = unitOfWork;
            _attractionService = attractionService;
            _fairyCharacterService = fairyCharacterService;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task<OrderModel> GetOrderByIdAsync(Guid id)
        {
            return await _unitOfWork.Orders.GetByIdAsync(id);
        }

        public async Task CreateOrderAsync(OrderModel model)
        {
            model.TotalPrice = await CalculateTotalPriceAsync(model);
            await _unitOfWork.Orders.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null) return false;

            await _unitOfWork.Orders.DeleteAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateOrderAsync(OrderModel model)
        {
            var existingOrder = await _unitOfWork.Orders.GetByIdAsync(model.Id);
            if (existingOrder == null) return false;

            existingOrder.OrderDate = model.OrderDate;
            existingOrder.EventProgramId = model.EventProgramId;
            existingOrder.EventProgram = model.EventProgram;

            existingOrder.OrderZones = model.OrderZones;
            existingOrder.OrderAttractions = model.OrderAttractions;
            existingOrder.OrderFairyCharacters = model.OrderFairyCharacters;

            existingOrder.TotalPrice = await CalculateTotalPriceAsync(existingOrder);

            await _unitOfWork.Orders.UpdateAsync(existingOrder);
            await _unitOfWork.SaveAsync();
            return true;
        }

        private async Task<decimal> CalculateTotalPriceAsync(OrderModel order)
        {
            decimal total = 0;
            if (order.OrderAttractions != null)
            {
                foreach (var orderAttraction in order.OrderAttractions)
                {
                    var attraction = await _attractionService.GetAttractionByIdAsync(orderAttraction.AttractionId);
                    total += attraction?.Price ?? 0;
                }
            }

            if (order.OrderFairyCharacters != null)
            {
                foreach (var orderFairyCharacter in order.OrderFairyCharacters)
                {
                    var fairyCharacter = await _fairyCharacterService.GetFairyCharacterByIdAsync(orderFairyCharacter.FairyCharacterId);
                    total += fairyCharacter?.Price ?? 0;
                }
            }
            return total;
        }

        public async Task<bool> AddZoneToOrder(Guid orderId, Guid zoneId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) return false;

            var zone = await _unitOfWork.Zones.GetByIdAsync(zoneId);
            if (zone == null) return false;

            order.OrderZones.Add(new OrderZoneModel { ZoneId = zoneId });
            order.TotalPrice = await CalculateTotalPriceAsync(order);

            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> AddAttractionToOrder(Guid orderId, Guid attractionId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) return false;

            var attraction = await _unitOfWork.Attractions.GetByIdAsync(attractionId);
            if (attraction == null) return false;

            order.OrderAttractions.Add(new OrderAttractionModel { AttractionId = attractionId });
            order.TotalPrice = await CalculateTotalPriceAsync(order);

            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> AddFairyCharacterToOrder(Guid orderId, Guid fairyCharacterId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) return false;

            var fairyCharacter = await _unitOfWork.FairyCharacters.GetByIdAsync(fairyCharacterId);
            if (fairyCharacter == null) return false;

            order.OrderFairyCharacters.Add(new OrderFairyCharacterModel { FairyCharacterId = fairyCharacterId });
            order.TotalPrice = await CalculateTotalPriceAsync(order);

            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveZoneFromOrder(Guid orderId, Guid zoneId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null || order.OrderZones == null) return false;

            var zone = order.OrderZones.FirstOrDefault(z => z.ZoneId == zoneId);
            if (zone == null) return false;

            order.OrderZones.Remove(zone);
            order.TotalPrice = await CalculateTotalPriceAsync(order);

            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAttractionFromOrder(Guid orderId, Guid attractionId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null || order.OrderAttractions == null) return false;

            var attraction = order.OrderAttractions.FirstOrDefault(a => a.Attraction?.Id == attractionId);
            if (attraction == null) return false;

            order.OrderAttractions.Remove(attraction);
            order.TotalPrice = await CalculateTotalPriceAsync(order);

            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveFairyCharacterFromOrder(Guid orderId, Guid fairyCharacterId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null || order.OrderFairyCharacters == null) return false;

            var fairyCharacter = order.OrderFairyCharacters.FirstOrDefault(f => f.FairyCharacter?.Id == fairyCharacterId);
            if (fairyCharacter == null) return false;

            order.OrderFairyCharacters.Remove(fairyCharacter);
            order.TotalPrice = await CalculateTotalPriceAsync(order);

            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
