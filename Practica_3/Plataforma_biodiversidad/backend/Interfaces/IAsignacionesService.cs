using backend.asignaciones;
using backend.asignacionesDTO;

namespace backend.Services;

public interface IAsignacionesService
{
  Task<IEnumerable<AsignacionesDTO>> ObtenerTodosAsync();
  Task<AsignacionesDTO?> ObtenerPorIdAsync(int id);
  Task<AsignacionesDTO> CrearAsync(AsignacionesDTO dto);
  Task UpdateAsync(int id, AsignacionesDTO dto);
  Task DeleteAsync(int id);
  Task<IEnumerable<AsignacionesDTO>> BuscarPorRolAsync(Rol rol);
  Task<IEnumerable<AsignacionesDTO>> BuscarPorFechaEntrada(DateOnly fecha);
}