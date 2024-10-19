using Microsoft.EntityFrameworkCore;
using SportsNewsWebApp.Models;

namespace SportsNewsWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Notification> Notifications { get; set; }
    }
}
