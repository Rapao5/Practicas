using backend.Interfaces;
using backend.investigador;
using backend.investigadorDTO;

namespace backend.Services;

public class InvestigadorService : IInvestigadorService
{
  private readonly IInvestigadorRepository repository;
  public InvestigadorService(IInvestigadorRepository repository)
  {
    this.repository=repository;
  }
  public async Task<IEnumerable<InvestigadorDTO>> ObtenerTodosAsync()
  {
    var investigadores = await repository.GetInvestigadorAsync();
    var investigadoresDTO = investigadores.Select(i => new InvestigadorDTO
    {
      Id=i.Id,
      Nombre = i.Nombre,
      Email = i.Email,
      Experiencia = i.Experiencia.ToString(),
      Proyectos = i.Asignaciones?.Select(a => a.Proyecto?.Nombre).ToList() ?? new List<string>()
    }).ToList();

    return investigadoresDTO;
  }

  public async Task<InvestigadorDTO> ObtenerPorIdAsync(int id)
  {
    var investigador = await repository.GetByIdAsync(id);
    if(investigador == null) return null;
    var investigadorDTO = new InvestigadorDTO
    {
      Id=investigador.Id,
      Nombre = investigador.Nombre,
      Email = investigador.Email,
      Experiencia = investigador.Experiencia.ToString(),
      Proyectos = investigador.Asignaciones?.Select(a => a.Proyecto?.Nombre).ToList() ?? new List<string>()
    };
    
    return investigadorDTO;
  }

  public async Task<InvestigadorDTO> CrearAsync(InvestigadorDTO dto)
  {
    var nuevoInvestigador = new Investigador
      {
        Nombre = dto.Nombre,
        Email = dto.Email,
        Experiencia = Enum.Parse<Experiencia>(dto.Experiencia)
      };

      await repository.AddAsync(nuevoInvestigador);
      dto.Id = nuevoInvestigador.Id;
      return dto;
  }

  public async Task UpdateAsync(int id, InvestigadorDTO dto)
  {
    if (id != dto.Id) return;

    var nuevoInvestigador = await repository.GetByIdAsync(id);
    if(nuevoInvestigador==null) return;

    nuevoInvestigador.Nombre=dto.Nombre;
    nuevoInvestigador.Email=dto.Email;
    nuevoInvestigador.Experiencia=Enum.Parse<Experiencia>(dto.Experiencia);

    await repository.Update(nuevoInvestigador);
  }

  public async Task DeleteAsync(int id)
  {
    var investigador = await repository.GetByIdAsync(id);
    
    if(investigador == null) return;

    await repository.Delete(id);
  }

  public async Task<IEnumerable<InvestigadorDTO>> BuscarPorNombreAsync(string nombre)
  {
    var investigadores = await repository.GetInvestigadorByNombre(nombre);
    if(investigadores==null) return Enumerable.Empty<InvestigadorDTO>();
    var investigadoresDTO = investigadores.Select(i => new InvestigadorDTO
    {
      Id = i.Id,
      Nombre = i.Nombre,
      Email = i.Email,
      Experiencia = i.Experiencia.ToString(),
      Proyectos = i.Asignaciones?.Select(a => a.Proyecto.Nombre).ToList() ?? new List<string>()
    });

    return investigadoresDTO;
  }

  public async Task<IEnumerable<InvestigadorDTO>> BuscarPorExperiencia(string experiencia)
  {
    var investigadores = await repository.GetInvestigadorByExperiencia(Enum.Parse<Experiencia>(experiencia));
    if(investigadores == null) return Enumerable.Empty<InvestigadorDTO>();
    var investigadoresDTO = investigadores.Select(i => new InvestigadorDTO
    {
      Id = i.Id,
      Nombre = i.Nombre,
      Email = i.Email,
      Experiencia = i.Experiencia.ToString(),
      Proyectos = i.Asignaciones?.Select(p => p.Proyecto.Nombre).ToList() ?? new List<string>()
    });

    return investigadoresDTO;
  }

  public async Task<IEnumerable<InvestigadorDTO>> BuscarPorProyecto(int id)
  {
    var investigador = await repository.GetInvestigadorByProyecto(id);
    if(investigador==null) return Enumerable.Empty<InvestigadorDTO>();

    var investigadorDTO = investigador.Select(i => new InvestigadorDTO
    {
      Id = i.Id,
      Nombre = i.Nombre,
      Email = i.Email,
      Experiencia = i.Experiencia.ToString(),
      Proyectos = i.Asignaciones?.Select(p => p.Proyecto.Nombre).ToList() ?? new List<string>()
    });

    return investigadorDTO;
  }
}