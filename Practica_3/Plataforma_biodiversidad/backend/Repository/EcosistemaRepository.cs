using backend.appDbContext;
using backend.eco;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.ecosistemaRepository;

public class EcosistemaRepository : IEcosistemaRepository
{
  private readonly AppDbContext context;

  public EcosistemaRepository(AppDbContext context)
  {
      this.context=context;  
  }
  public async Task<IEnumerable<Ecosistema>> GetEcosistemasPaginadosAsync(int skip, int take)
  {
    return await context.Ecosistemas
                        .Include(e => e.Proyectos)
                        .OrderBy(p => p.Descripcion) 
                        .Skip(skip)            
                        .Take(take)           
                        .ToListAsync();
  }
  public async Task<IEnumerable<Ecosistema>> GetEcosistemaAsync()
  {
    return await context.Ecosistemas
                        .AsNoTracking()
                        .Include(e => e.Proyectos)
                        .ToListAsync();
  }
  public async Task<Ecosistema> GetByIdAsync(int id)
  {
    return await context.Ecosistemas
                  .Include(e => e.Proyectos)
                  .FirstOrDefaultAsync(e => e.Id == id);
  }
  public async Task<Ecosistema> GetByIdBasicAsync(int id)
  {
    return await context.Ecosistemas
                  .FirstOrDefaultAsync(e => e.Id == id);
  }
  public async Task AddAsync(Ecosistema ecosistema)
  {
    await context.Ecosistemas.AddAsync(ecosistema);
    await context.SaveChangesAsync();
  }
  public async Task Delete(int id)
  {
    await context.Ecosistemas
                  .Where(e => e.Id == id)
                  .ExecuteDeleteAsync();
  }
  public async Task Update(Ecosistema ecosistema)
  {
    context.Ecosistemas.Update(ecosistema);
    await context.SaveChangesAsync();
  }
  public async Task<Ecosistema> GetAreaEcosistema(decimal areaLatitud, decimal areaLongitud)
  {
    return await context.Ecosistemas
                  .Where(e => e.AreaLatitud == areaLatitud)
                  .Where(e => e.AreaLongitud == areaLongitud)
                  .FirstOrDefaultAsync();
  }
  public async Task<IEnumerable<Ecosistema>> GetConservacion(Conservacion conservacion)
  {
    return await context.Ecosistemas
                        .AsNoTracking()
                        .Where(e => e.Conservacion == conservacion)
                        .ToListAsync();
  }
}