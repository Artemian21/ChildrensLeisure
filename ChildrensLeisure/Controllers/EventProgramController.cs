using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChildrensLeisure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventProgramController : ControllerBase
    {
        private readonly IEventProgramService _eventProgramService;

        public EventProgramController(IEventProgramService eventProgramService)
        {
            _eventProgramService = eventProgramService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventProgramModel>>> GetAllEventPrograms()
        {
            var eventPrograms = await _eventProgramService.GetAllEventProgramsAsync();
            return Ok(eventPrograms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventProgramModel>> GetEventProgramById(Guid id)
        {
            var eventProgram = await _eventProgramService.GetEventProgramByIdAsync(id);
            if (eventProgram == null)
                return NotFound();

            return Ok(eventProgram);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEventProgram([FromBody] EventProgramModel model)
        {
            if (model == null)
                return BadRequest();

            await _eventProgramService.CreateEventProgramAsync(model);
            return CreatedAtAction(nameof(GetEventProgramById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEventProgram(Guid id, [FromBody] EventProgramModel model)
        {
            model.Id = id;
            if (model == null || id != model.Id)
                return BadRequest();

            await _eventProgramService.UpdateEventProgramAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEventProgram(Guid id)
        {
            var deleted = await _eventProgramService.DeleteEventProgramAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
