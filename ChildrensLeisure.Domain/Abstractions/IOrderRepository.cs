using ChildrensLeisure.Domain.Models;

namespace ChildrensLeisure.Domain.Abstractions
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderModel entity);
        Task DeleteAsync(OrderModel entity);
        Task UpdateAsync(OrderModel entity);
        Task<IEnumerable<OrderModel>> GetAllAsync();
        Task<OrderModel> GetByIdAsync(Guid id);
    }
}