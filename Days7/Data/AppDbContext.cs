using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySql("server=localhost;database=todoapp;user=root;password=", new MySqlServerVersion(new Version(8, 0, 36)));
}
