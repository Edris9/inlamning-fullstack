using HabitTracker.API.DTOs;
using HabitTracker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitsController : ControllerBase
    {
        private readonly IHabitService _service;

        public HabitsController(IHabitService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var habits = await _service.GetAllAsync();
            return Ok(habits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var habit = await _service.GetByIdAsync(id);
            if (habit == null) return NotFound($"Habit med id {id} hittades inte.");
            return Ok(habit);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHabitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var habit = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = habit.Id }, habit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHabitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var habit = await _service.UpdateAsync(id, dto);
            if (habit == null) return NotFound($"Habit med id {id} hittades inte.");
            return Ok(habit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound($"Habit med id {id} hittades inte.");
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var habit = await _service.CompleteAsync(id);
            if (habit == null) return NotFound($"Habit med id {id} hittades inte.");
            return Ok(habit);
        }
    }
}