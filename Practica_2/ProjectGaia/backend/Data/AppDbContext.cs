using Microsoft.EntityFrameworkCore;
using backend.Model;

namespace backend.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Sensor> Sensors { get; set; }
  }
}