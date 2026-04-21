using backend.investigador;

namespace backend.Interfaces;

public interface IInvestigadorRepository
{
  Task<IEnumerable<Investigador>> GetInvestigadoresPaginadosAsync(int skip, int take);
  Task<IEnumerable<Investigador>> GetInvestigadorAsync();
  Task<Investigador> GetByIdAsync(int id);
  Task AddAsync(Investigador investigador);
  Task Delete(int id);
  Task Update(Investigador investigador);
  Task<IEnumerable<Investigador>> GetInvestigadorByNombre(string nombre);
  Task<IEnumerable<Investigador>> GetInvestigadorByExperiencia(Experiencia experiencia);
  Task<IEnumerable<Investigador>> GetInvestigadorByProyecto(int proyectoId);
}