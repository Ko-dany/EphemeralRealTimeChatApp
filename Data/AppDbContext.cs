using EphemeralRealTimeChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EphemeralRealTimeChatApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
