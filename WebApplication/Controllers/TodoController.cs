using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        #region Fields
        private readonly TodoDataContext _dbContext;
        #endregion

        #region Ctors
        public TodoController(TodoDataContext context)
        {
            _dbContext = context;
            if (!_dbContext.TodoItems.Any())
            {
                _dbContext.TodoItems.Add(new TodoItem { Name = "Item1" });
                _dbContext.TodoItems.Add(new TodoItem { Name = "Item2" });
                _dbContext.TodoItems.Add(new TodoItem { Name = "Item3" });
                _dbContext.SaveChanges();
            }
        }
        #endregion

        #region APIs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoList()
        {
            return await _dbContext.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoById(long id)
        {
            var todoItem = await _dbContext.TodoItems.FindAsync(id);
            if (todoItem == null)
                return NotFound();
            return todoItem;
        }
        
        [HttpPost]
        public async Task<ActionResult<TodoItem>> AddTodoItem(TodoItem item)
        {
            _dbContext.TodoItems.Add(item);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _dbContext.TodoItems.Remove(item);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}