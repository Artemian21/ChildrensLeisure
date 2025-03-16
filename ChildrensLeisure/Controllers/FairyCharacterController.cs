using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChildrensLeisure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FairyCharacterController : ControllerBase
    {
        private readonly IFairyCharacterService _fairyCharacterService;

        public FairyCharacterController(IFairyCharacterService fairyCharacterService)
        {
            _fairyCharacterService = fairyCharacterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FairyCharacterModel>>> GetAllFairyCharacters()
        {
            var fairyCharacters = await _fairyCharacterService.GetAllFairyCharactersAsync();
            return Ok(fairyCharacters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FairyCharacterModel>> GetFairyCharacterById(Guid id)
        {
            var fairyCharacter = await _fairyCharacterService.GetFairyCharacterByIdAsync(id);
            if (fairyCharacter == null)
                return NotFound();

            return Ok(fairyCharacter);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFairyCharacter([FromBody] FairyCharacterModel model)
        {
            if (model == null)
                return BadRequest();

            await _fairyCharacterService.CreateFairyCharacterAsync(model);
            return CreatedAtAction(nameof(GetFairyCharacterById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFairyCharacter(Guid id, [FromBody] FairyCharacterModel model)
        {
            model.Id = id;
            if (model == null || id != model.Id)
                return BadRequest();

            await _fairyCharacterService.UpdateFairyCharacterAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFairyCharacter(Guid id)
        {
            var deleted = await _fairyCharacterService.DeleteFairyCharacterAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
