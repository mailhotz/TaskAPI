using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;
        private int Id = 1;

        public TaskController(TaskContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            return await _context.TaskItems.Where(t => t.IsDeleted == false).ToListAsync();
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null || taskItem.IsDeleted)
            {
                return NotFound();
            }

            return taskItem;
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask(TaskItem taskItem)
        {
            //Note: This is bad and would likely be fixed by using GUID's instead
            taskItem.Id = Id;
            Id++;
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetTaskById), new { id = taskItem.Id }, taskItem);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.IsDeleted = true;
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
