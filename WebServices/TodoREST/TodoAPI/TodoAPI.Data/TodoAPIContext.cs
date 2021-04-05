using Microsoft.EntityFrameworkCore;
using TodoAPI.Domain;

namespace TodoAPI.Data
{
    public class TodoAPIContext : DbContext
    {
        public TodoAPIContext(DbContextOptions<TodoAPIContext> options) : base(options)
        {

        }
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
