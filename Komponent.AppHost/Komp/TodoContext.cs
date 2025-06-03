// Data/TodoContext.cs
using Microsoft.EntityFrameworkCore;
using TodoAppv2.Komp;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }  // Kolekcja zadań (odpowiada tabeli w DB)
}
