using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApiDb.Data;
using TodoApiDb.Models;

namespace TodoApiDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private ITodoItemsService _todoItemsService;

        public TodoItemsController(
            TodoContext context) 
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems
        [HttpGet("GetTodoItems2")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems2()
        {
            string[] myAnimals = { "dog", "cat" };

            _todoItemsService = new TodoItemsService(_context.TodoItems);

            _todoItemsService.GetAbunch(myAnimals);
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpGet("GetABunch")]
        public async Task<ActionResult<TodoItem>> GetABunch()
        {
            string[] myAnimals = { "dog", "cat" };

            //works in MS SQL 2017
            var animals = await _context.TodoItems
                .Where(i => myAnimals.Contains(i.Name))
                .ToListAsync();

            return Ok(animals);
        }

        [HttpGet("GetABunchV2")]
        public async Task<ActionResult<TodoItem>> GetABunchV2()
        {
            string[] myAnimals = { "dog", "cat" };

            //works in MS SQL 2017
            var animals = await _context.TodoItems
                .Where(i => myAnimals.Contains(i.Name))
                .ToListAsync();

            return Ok(animals);
        }

        [HttpGet("GetABunch120")]
        public async Task<ActionResult<TodoItem>> GetABunch120()
        {
            string[] myAnimals = { "dog", "cat" };

            //works in MS SQL 2014
            var animals = await _context.TodoItems
                .Where(i => EF.Constant(myAnimals).Contains(i.Name))
                .ToListAsync();

            return Ok(animals);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
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

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
