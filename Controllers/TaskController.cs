using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTOS;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<TaskReadDto>>> GetTasks(CancellationToken ct)
        {

            var items = await _context.Tasks
                            .AsNoTracking()
                            .OrderByDescending(t => t.Id)
                            .Select(t => new TaskReadDto
                            {
                                Id = t.Id,
                                Title = t.Title,
                                IsCompleted = t.IsCompleted
                            })
                            .ToListAsync(ct);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> GetTaskById(int id, CancellationToken ct)
        {
            var task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if (task == null) return NotFound();

            return Ok(ToReadDto(task));
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto task, CancellationToken ct)
        {
            var izBazeTask = await _context.Tasks.FindAsync(id);

            if (izBazeTask == null)
                return NotFound();

            izBazeTask.Title = task.Title;
            izBazeTask.Description = task.Description;
            izBazeTask.IsCompleted = task.IsCompleted;

            await _context.SaveChangesAsync(ct);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTask(int id, CancellationToken ct)
        {

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TaskCreateDto>> CreateTask([FromBody] TaskCreateDto task, CancellationToken ct)
        {
            var entity = new TaskItem
            {
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted
            };

            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = entity.Id }, entity);
        }

        private static TaskReadDto ToReadDto(TaskItem t) => new()
        {
            Id = t.Id,
            Title = t.Title,
            IsCompleted = t.IsCompleted
        };

    }
}
