using Microsoft.EntityFrameworkCore;

namespace TodoApiDb.Models;

public class TodoContext : DbContext
{
    public TodoContext(): base()
    {
    }

    public TodoContext(DbContextOptions<TodoContext> options): base(options)
    {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; } = null!;
}