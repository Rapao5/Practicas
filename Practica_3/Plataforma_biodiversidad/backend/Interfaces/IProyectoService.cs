using backend.proyectoDTO;

namespace backend.Services;

public interface IProyectoService
{
  Task<IEnumerable<ProyectoDTO>> ObtenerTodosAsync();
  Task<ProyectoDTO?> ObtenerPorIdAsync(int id);
  Task<ProyectoDTO> CrearAsync(ProyectoDTO dto);
  Task UpdateAsync(int id, ProyectoDTO dto);
  Task DeleteAsync(int id);
  Task<IEnumerable<ProyectoDTO>> BuscarPorEspecieFoco(string especieFoco);
  Task<IEnumerable<ProyectoDTO>> BuscarPorEstado(bool estado);
  Task<IEnumerable<ProyectoDTO>> BuscarPorEcosistema(int ecosistemaId);
}