using backend.asignaciones;

namespace backend.Interfaces;

public interface IAsignacionesRepository
{
  Task<IEnumerable<Asignaciones>> GetAsignacionesAsync();
  Task<Asignaciones> GetByIdAsync(int id);
  Task AddAsync(Asignaciones asignaciones);
  Task Delete(int id);
  Task Update(Asignaciones asignaciones);
  Task<IEnumerable<Asignaciones>> ObtenerPorProyectoAsync(int proyectoId);
  Task<IEnumerable<Asignaciones>> GetRol(Rol rol);
  Task<IEnumerable<Asignaciones>> GetFechaEntrada(DateOnly fecha);

}