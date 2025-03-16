using ChildrensLeisure.DataAccess.Entity;
using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ChildrensLeisure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var jsonResponse = JsonSerializer.Serialize(orders, options);

            return Ok(jsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var jsonResponse = JsonSerializer.Serialize(order, options);

            return Ok(jsonResponse);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderModel model)
        {
            if (model == null)
                return BadRequest();

            await _orderService.CreateOrderAsync(model);
            return CreatedAtAction(nameof(GetOrderById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(Guid id, [FromBody] OrderModel model)
        {
            model.Id = id;
            if (model == null || id != model.Id)
                return BadRequest();

            var updated = await _orderService.UpdateOrderAsync(model);
            if (!updated)
                return NotFound();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{orderId}/zones")]
        public async Task<ActionResult> AddZoneToOrder(Guid orderId, [FromBody] Guid zoneId)
        {
            var added = await _orderService.AddZoneToOrder(orderId, zoneId);
            if (!added)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{orderId}/zones/{zoneId}")]
        public async Task<ActionResult> RemoveZoneFromOrder(Guid orderId, Guid zoneId)
        {
            var removed = await _orderService.RemoveZoneFromOrder(orderId, zoneId);
            if (!removed)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{orderId}/attractions")]
        public async Task<ActionResult> AddAttractionToOrder(Guid orderId, [FromBody] Guid attractionId)
        {
            var added = await _orderService.AddAttractionToOrder(orderId, attractionId);
            if (!added)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{orderId}/attractions/{attractionId}")]
        public async Task<ActionResult> RemoveAttractionFromOrder(Guid orderId, Guid attractionId)
        {
            var removed = await _orderService.RemoveAttractionFromOrder(orderId, attractionId);
            if (!removed)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{orderId}/fairy-characters")]
        public async Task<ActionResult> AddFairyCharacterToOrder(Guid orderId, [FromBody] Guid fairyCharacterId)
        {
            var added = await _orderService.AddFairyCharacterToOrder(orderId, fairyCharacterId);
            if (!added)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{orderId}/fairy-characters/{fairyCharacterId}")]
        public async Task<ActionResult> RemoveFairyCharacterFromOrder(Guid orderId, Guid fairyCharacterId)
        {
            var removed = await _orderService.RemoveFairyCharacterFromOrder(orderId, fairyCharacterId);
            if (!removed)
                return NotFound();

            return NoContent();
        }
    }
}
