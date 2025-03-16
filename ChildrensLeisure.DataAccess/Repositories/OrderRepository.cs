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
    public class OrderRepository : IOrderRepository
    {
        private readonly ChildrensLeisureDBContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ChildrensLeisureDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(OrderModel entity)
        {
            var order = _mapper.Map<Order>(entity);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderModel entity)
        {
            var order = _mapper.Map<Order>(entity);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderModel entity)
        {
            var order = _mapper.Map<Order>(entity);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderZones)
                .Include(o => o.OrderAttractions)
                .Include(o => o.OrderFairyCharacters)
                .Include(o => o.EventProgram)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }

        public async Task<OrderModel> GetByIdAsync(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderZones)
                .Include(o => o.OrderAttractions)
                .Include(o => o.OrderFairyCharacters)
                .Include(o => o.EventProgram)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return _mapper.Map<OrderModel>(order);
        }
    }
}
