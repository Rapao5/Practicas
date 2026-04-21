using backend.investigadorDTO;

namespace backend.Services;

public interface IInvestigadorService
{
  Task<IEnumerable<InvestigadorDTO>> ObtenerTodosPaginadosAsync(int skip, int take);
  Task<IEnumerable<InvestigadorDTO>> ObtenerTodosAsync();
  Task<InvestigadorDTO?> ObtenerPorIdAsync(int id);
  Task<InvestigadorDTO> CrearAsync(InvestigadorDTO dto);
  Task UpdateAsync(int id, InvestigadorDTO dto);
  Task DeleteAsync(int id);
  Task<IEnumerable<InvestigadorDTO>> BuscarPorNombreAsync(string nombre);
  Task<IEnumerable<InvestigadorDTO>> BuscarPorExperiencia(string experiencia);
  Task<IEnumerable<InvestigadorDTO>> BuscarPorProyecto(int id);
}