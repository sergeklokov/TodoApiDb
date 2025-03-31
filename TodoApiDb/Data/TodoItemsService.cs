using Microsoft.EntityFrameworkCore;
using TodoApiDb.Models;

namespace TodoApiDb.Data
{
    public class TodoItemsService: ITodoItemsService
    {
        //private readonly DbSet<TodoItem> _todoItems;

        //public TodoItemsService(DbSet<TodoItem> todoItems) 
        //{
        //    _todoItems = todoItems;
        //}

        //List<TodoItem>? ITodoItemsService.GetAbunch(string[] myAnimals)
        //{
        //    var animals = _todoItems.Where(i => myAnimals.Contains(i.Name))
        //        .ToList();

        //    return animals;
        //}

        private readonly TodoContext _context;

        public TodoItemsService(TodoContext context)
        {
            _context = context;
        }

        List<TodoItem>? ITodoItemsService.GetAbunch(string[] myAnimals)
        {
            var animals = _context.TodoItems.Where(i => myAnimals.Contains(i.Name))
                .ToList();

            return animals;
        }
    }
}
