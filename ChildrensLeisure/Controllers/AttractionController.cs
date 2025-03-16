using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChildrensLeisure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttractionController : ControllerBase
    {
        private readonly IAttractionService _attractionService;

        public AttractionController(IAttractionService attractionService)
        {
            _attractionService = attractionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttractions()
        {
            var attractions = await _attractionService.GetAllAttractionsAsync();
            return Ok(attractions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAttractions(Guid id)
        {
            var attraction = await _attractionService.GetAttractionByIdAsync(id);
            if (attraction == null) return NotFound();
            return Ok(attraction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttractions([FromBody] AttractionModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _attractionService.CreateAttractionAsync(model);
            return CreatedAtAction(nameof(GetByIdAttractions), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttractions(Guid id, [FromBody] AttractionModel model)
        {
            model.Id = id;
            if (model == null || id != model.Id)
                return BadRequest();

            var updated = await _attractionService.UpdateAttractionAsync(model);
            if (!updated) return NotFound();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttractions(Guid id)
        {
            var deleted = await _attractionService.DeleteAttractionAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
