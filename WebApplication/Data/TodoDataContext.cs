using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class TodoDataContext : DbContext
    {
        public TodoDataContext(DbContextOptions<TodoDataContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
