using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
                _dbContext.SaveChanges();
            }
        }
        #endregion

        #region Apis
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
        #endregion
    }
}