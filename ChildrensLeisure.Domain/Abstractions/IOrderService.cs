using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
        Task<OrderModel> GetOrderByIdAsync(Guid id);
        Task CreateOrderAsync(OrderModel model);
        Task<bool> DeleteOrderAsync(Guid id);
        Task<bool> UpdateOrderAsync(OrderModel model);
        Task<bool> AddZoneToOrder(Guid orderId, Guid zoneId);
        Task<bool> RemoveZoneFromOrder(Guid orderId, Guid zoneId);
        Task<bool> AddAttractionToOrder(Guid orderId, Guid attractionId);
        Task<bool> RemoveAttractionFromOrder(Guid orderId, Guid attractionId);
        Task<bool> AddFairyCharacterToOrder(Guid orderId, Guid fairyCharacterId);
        Task<bool> RemoveFairyCharacterFromOrder(Guid orderId, Guid fairyCharacterId);
    }
}
