using CommandCliApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandCliApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Command> Commands { get; set; }
    }
}
