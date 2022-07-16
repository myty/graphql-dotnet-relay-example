using Microsoft.EntityFrameworkCore;
using Models = GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Services
{
    public class TodoContext : DbContext
    {
        public DbSet<Models.Todo> Todos { get; set; }
        public DbSet<Models.User> Users { get; set; }

        public string DbPath { get; }

        public TodoContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "todo.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().HasIndex(b => b.Username).IsUnique();
        }
    }
}
