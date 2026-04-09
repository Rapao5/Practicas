using backend.proyecto;

namespace backend.Interfaces;

public interface IProyectoRepository
{
  Task<IEnumerable<Proyecto>> GetProyectosAsync();
  Task<Proyecto> GetByIdAsync(int id);
  Task AddAsync(Proyecto proyecto);
  Task Delete(int id);
  Task Update(Proyecto proyecto);
  Task<IEnumerable<Proyecto>> GetProyectoByEspecieFoco(string especieFoco);
  Task<IEnumerable<Proyecto>> GetProyectoByEstado(bool estado);
  Task<IEnumerable<Proyecto>> GetProyectoByEcosistema(int ecosistemaId);
}