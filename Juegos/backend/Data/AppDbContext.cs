using Microsoft.EntityFrameworkCore;
using backend.game;
using backend.player;

namespace backend.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Game> Games {get; set;}
    public DbSet<Player> Players {get; set;}
  }
}