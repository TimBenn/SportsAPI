using Microsoft.EntityFrameworkCore;
using SportsAPI.Models;

namespace SportsAPI.Context
{
    public class SportsContext : DbContext
    {
        public SportsContext(DbContextOptions<SportsContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Player> Players { get; set; }
    }
}
