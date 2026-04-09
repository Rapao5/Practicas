using backend.eco;

namespace backend.Interfaces;

public interface IEcosistemaRepository
{
  Task<IEnumerable<Ecosistema>> GetEcosistemaAsync();
  Task<Ecosistema> GetByIdAsync(int id);
  Task AddAsync(Ecosistema ecosistema);
  Task Delete(int id);
  Task Update(Ecosistema ecosistema);
  Task<Ecosistema> GetAreaEcosistema(decimal areaLatitud, decimal areaLongitud);
  Task<IEnumerable<Ecosistema>> GetConservacion(Conservacion conservacion);
}