using backend.ecoDTO;

namespace backend.Services;

public interface IEcosistemaService
{
  Task<IEnumerable<EcosistemaDTO>> ObtenerTodosAsync();
  Task<EcosistemaDTO?> ObtenerPorIdAsync(int id);
  Task<EcosistemaDTO> CrearAsync(EcosistemaDTO dto);
  Task UpdateAsync(int id, EcosistemaDTO dto);
  Task DeleteAsync(int id);
  Task<EcosistemaDTO> BuscarPorArea(decimal areaLatitud, decimal areaLongitud);
  Task<IEnumerable<EcosistemaDTO>> BuscarPorConservacion(string conservacion);
}