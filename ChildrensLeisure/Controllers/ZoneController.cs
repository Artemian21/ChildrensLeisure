using ChildrensLeisure.DataAccess.Entity;
using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChildrensLeisure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _zoneService;

        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZoneModel>>> GetAllZones()
        {
            var zones = await _zoneService.GetAllZonesAsync();
            return Ok(zones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ZoneModel>> GetZoneById(Guid id)
        {
            var zone = await _zoneService.GetZoneByIdAsync(id);
            if (zone == null)
                return NotFound();
            return Ok(zone);
        }

        [HttpPost]
        public async Task<ActionResult> CreateZone([FromBody] ZoneModel model)
        {
            if (model == null)
                return BadRequest();

            await _zoneService.CreateZoneAsync(model);
            return CreatedAtAction(nameof(GetZoneById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateZone(Guid id, [FromBody] ZoneModel model)
        {
            model.Id = id;
            if (model == null || id != model.Id)
                return BadRequest();

            await _zoneService.UpdateZoneAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteZone(Guid id)
        {
            var deleted = await _zoneService.DeleteZoneAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
