using backend.appDbContext;
using backend.Interfaces;
using backend.investigador;
using Microsoft.EntityFrameworkCore;

namespace backend.investigadorRepository;

public class InvestigadorRepository : IInvestigadorRepository
{
  private readonly AppDbContext context;

  public InvestigadorRepository(AppDbContext context)
  {
    this.context = context;
  }
  public async Task<IEnumerable<Investigador>> GetInvestigadoresPaginadosAsync(int skip, int take)
  {
    return await context.Investigadores
                        .Include(i => i.Asignaciones) 
                            .ThenInclude(a => a.Proyecto)
                          .OrderBy(p => p.Nombre) 
                        .Skip(skip)            
                        .Take(take)           
                        .ToListAsync();
  }
  public async Task<IEnumerable<Investigador>> GetInvestigadorAsync()
  {
    return await context.Investigadores
                      .AsNoTracking()
                      .Include(i => i.Asignaciones) 
                            .ThenInclude(a => a.Proyecto)
                      .ToListAsync();
  }
  public async Task<Investigador> GetByIdAsync(int id)
  {
    return await context.Investigadores
                        .AsNoTracking()
                        .Include(i => i.Asignaciones) 
                            .ThenInclude(a => a.Proyecto) 
                        .FirstOrDefaultAsync(i => i.Id == id);
  }
  public async Task AddAsync(Investigador investigador)
  {
    await context.Investigadores.AddAsync(investigador);
    await context.SaveChangesAsync();
  }
  public async Task Delete(int id)
  {
    await context.Investigadores
                  .Where(i => i.Id == id)
                  .ExecuteDeleteAsync();
  }
  public async Task Update(Investigador investigador)
  {
    context.Investigadores.Update(investigador);
    await context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Investigador>> GetInvestigadorByNombre(string nombre)
  {
    return await context.Investigadores
                        .AsNoTracking()
                        .Include(i => i.Asignaciones) 
                            .ThenInclude(a => a.Proyecto)
                        .Where(i => i.Nombre == nombre)
                        .ToListAsync();
  }
  public async Task<IEnumerable<Investigador>> GetInvestigadorByExperiencia(Experiencia experiencia)
  {
    return await context.Investigadores
                        .AsNoTracking()
                        .Include(i => i.Asignaciones) 
                            .ThenInclude(a => a.Proyecto)
                        .Where(i => i.Experiencia == experiencia)
                        .ToListAsync();
  }
  public async Task<IEnumerable<Investigador>> GetInvestigadorByProyecto(int proyectoId)
  {
    return await context.Investigadores
                          .AsNoTracking()
                          .Include(i => i.Asignaciones) 
                            .ThenInclude(a => a.Proyecto)
                          .Where(i => i.Asignaciones.Any(a => a.ProyectoId == proyectoId))
                          .ToListAsync();
  }
}