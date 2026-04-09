using backend.appDbContext;
using backend.asignaciones;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.asignacionesRepository;

public class AsignacionesRepository : IAsignacionesRepository
{
  private readonly AppDbContext context;

  public AsignacionesRepository(AppDbContext context)
  {
    this.context = context;
  }
  public async Task<IEnumerable<Asignaciones>> GetAsignacionesAsync()
  {
    return await context.Asignaciones
                        .AsNoTracking()
                        .Include(a => a.Proyecto)
                        .Include(a => a.Investigador)
                        .ToArrayAsync();
  }
  public async Task<Asignaciones> GetByIdAsync(int id)
  {
    return await context.Asignaciones
                        .AsNoTracking()
                        .Include(a => a.Proyecto)
                        .Include(a => a.Investigador)
                        .FirstOrDefaultAsync(a => a.Id == id);
  }
  public async Task AddAsync(Asignaciones asignaciones)
  {
    await context.Asignaciones.AddAsync(asignaciones);
    await context.SaveChangesAsync();
  }
  public async Task Delete(int id)
  {
    await context.Asignaciones.Where(a => a.Id == id).ExecuteDeleteAsync();
  }
  public async Task Update(Asignaciones asignaciones)
  {
    context.Asignaciones.Update(asignaciones);
    await context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Asignaciones>> GetRol(string rol)
  {
    return await context.Asignaciones
                        .AsNoTracking()
                        .Include(a => a.Proyecto)
                        .Include(a => a.Investigador)
                        .Where(a => a.Rol == rol)
                        .ToListAsync();
  }
  public async Task<IEnumerable<Asignaciones>> GetFechaEntrada(DateOnly fecha)
  {
    return await context.Asignaciones
                        .AsNoTracking()
                        .Include(a => a.Proyecto)
                        .Include(a => a.Investigador)
                        .Where(a => a.FechaEntrada == fecha)
                        .ToListAsync();
  }
}