using TodoApiDb.Models;

namespace TodoApiDb.Data
{
    public interface ITodoItemsService
    {
        List<TodoItem>? GetAbunch(string[] myAnimals);
    }
}