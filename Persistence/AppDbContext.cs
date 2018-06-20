using Microsoft.EntityFrameworkCore;
using Scores.Models;

namespace Scores.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}