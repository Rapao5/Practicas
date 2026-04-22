using backend.appDbContext;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.proyecto;
namespace backend.proyectoRepository;

public class ProyectoRepository : IProyectoRepository
{
  private readonly AppDbContext context;

  public ProyectoRepository(AppDbContext context)
  {
    this.context = context;
  }

  public async Task<IEnumerable<Proyecto>> GetProyectosPaginadosAsync(int skip, int take)
{
    return await context.Proyectos
        .Include(p => p.Ecosistema)
        .Include(p => p.Asignaciones)
            .ThenInclude(a => a.Investigador)
        .OrderBy(p => p.Id) 
        .Skip(skip)            
        .Take(take)           
        .ToListAsync();
}

  public async Task<IEnumerable<Proyecto>> GetProyectosAsync()
  {
    return await context.Proyectos
            .AsNoTracking()
            .Include(p => p.Ecosistema)
            .Include(p => p.Asignaciones)
              .ThenInclude(a => a.Investigador)
            .ToListAsync();
  }
  public async Task<Proyecto> GetByIdAsync(int id)
  {
    return await context.Proyectos
            .AsNoTracking()
            .Include(p => p.Ecosistema)
            .Include(p => p.Asignaciones)
                .ThenInclude(a => a.Investigador)
            .FirstOrDefaultAsync(p => p.Id == id);
  }
  public async Task AddAsync(Proyecto proyecto)
  {
    await context.Proyectos.AddAsync(proyecto);
    await context.SaveChangesAsync();
  }
  public async Task Delete(int id)
  {
     await context.Proyectos
        .Where(p => p.Id == id)
        .ExecuteDeleteAsync();
  }
  public async Task Update(Proyecto proyecto)
  {
    context.Proyectos.Update(proyecto);
    await context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Proyecto>> GetProyectoByEspecieFoco(string especieFoco)
  {
    return await context.Proyectos
            .AsNoTracking()
            .Include(p => p.Ecosistema)
            .Include(p => p.Asignaciones)
                .ThenInclude(a => a.Investigador)
            .Where(p => p.EspecieFoco == especieFoco)
            .ToListAsync();
  }
  public async Task<IEnumerable<Proyecto>> GetProyectoByEstado(bool estado)
  {
    return await context.Proyectos
                .AsNoTracking()
                .Include(p => p.Ecosistema)
                .Include(p => p.Asignaciones)
                    .ThenInclude(a => a.Investigador)
                .Where(p => p.Estado == estado)
                .ToListAsync();
  }
  public async Task<IEnumerable<Proyecto>> GetProyectoByEcosistema(int ecosistemaId)
  {
    return await context.Proyectos
                  .AsNoTracking()
                  .Include(p => p.Ecosistema)
                  .Include(p => p.Asignaciones)
                    .ThenInclude(a => a.Investigador)
                  .Where(p => p.EcosistemaId == ecosistemaId)
                  .ToListAsync();
  }
}