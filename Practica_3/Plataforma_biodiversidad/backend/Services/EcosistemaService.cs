using backend.eco;
using backend.ecoDTO;
using backend.Interfaces;

namespace backend.Services;

public class EcosistemaService : IEcosistemaService
{
  private readonly IEcosistemaRepository repository;
  public EcosistemaService(IEcosistemaRepository repository)
  {
    this.repository = repository;
  }
  public async Task<IEnumerable<EcosistemaDTO>> ObtenerTodosPaginadosAsync(int skip, int take)
  {
    var ecosistemas = await repository.GetEcosistemasPaginadosAsync(skip, take);

    return ecosistemas.Select(e => new EcosistemaDTO
    {
      Id = e.Id,
      Descripcion = e.Descripcion,
      AreaLatitud = e.AreaLatitud,
      AreaLongitud = e.AreaLongitud,
      Conservacion = e.Conservacion.ToString(),
      Proyectos = e.Proyectos?.Select(p => p.Nombre).ToList() ?? new List<string>()
    }).ToList();
  }
  public async Task<IEnumerable<EcosistemaDTO>> ObtenerTodosAsync()
  {
    var ecosistemas = await repository.GetEcosistemaAsync();

    var ecosistemasDTO = ecosistemas.Select(e => new EcosistemaDTO
    {
      Id = e.Id,
      Descripcion = e.Descripcion,
      AreaLatitud = e.AreaLatitud,
      AreaLongitud = e.AreaLongitud,
      Conservacion = e.Conservacion.ToString(),
      Proyectos = e.Proyectos?.Select(p => p.Nombre).ToList() ?? new List<string>()
    }).ToList();

    return ecosistemasDTO;
  }

  public async Task<EcosistemaDTO> ObtenerPorIdAsync(int id)
  {
    var ecosistema = await repository.GetByIdAsync(id);

    if(ecosistema == null) return null;

    var ecosistemaDTO =new EcosistemaDTO
    {
      Id = ecosistema.Id,
      Descripcion = ecosistema.Descripcion,
      AreaLatitud = ecosistema.AreaLatitud,
      AreaLongitud = ecosistema.AreaLongitud,
      Conservacion = ecosistema.Conservacion.ToString(),
      Proyectos = ecosistema.Proyectos?.Select(p => p.Nombre).ToList() ?? new List<string>()
    };

    return ecosistemaDTO;
  }

  public async Task<EcosistemaDTO> CrearAsync(EcosistemaDTO dto)
  {
    var nuevoEco = new Ecosistema
    {
      Descripcion = dto.Descripcion,
      AreaLatitud = dto.AreaLatitud,
      AreaLongitud = dto.AreaLongitud,
      Conservacion = Enum.Parse<Conservacion>(dto.Conservacion)
    };
    await repository.AddAsync(nuevoEco);
    dto.Id = nuevoEco.Id;
    return dto;
  }

  public async Task UpdateAsync(int id, EcosistemaDTO dto)
  {
    if (id != dto.Id) return;

    var eco = await repository.GetByIdAsync(id);
    if(eco == null ) return;

      eco.Descripcion = dto.Descripcion;
      eco.AreaLatitud = dto.AreaLatitud;
      eco.AreaLongitud = dto.AreaLongitud;
      eco.Conservacion = Enum.Parse<Conservacion>(dto.Conservacion);

    await repository.Update(eco);
  }

  public async Task DeleteAsync(int id)
  {
    var ecosistema = await repository.GetByIdAsync(id);

    if(ecosistema == null) return;

    await repository.Delete(id);
  }

  public async Task<EcosistemaDTO> BuscarPorArea(decimal areaLatitud, decimal areaLongitud)
  {
    var ecosistemas = await repository.GetAreaEcosistema(areaLatitud, areaLongitud);

    if(ecosistemas == null) return null;

    var ecoDTO = new EcosistemaDTO
    {
      Id=ecosistemas.Id,
      Descripcion=ecosistemas.Descripcion,
      AreaLatitud=ecosistemas.AreaLatitud,
      AreaLongitud=ecosistemas.AreaLongitud,
      Conservacion=ecosistemas.Conservacion.ToString()
    };

    return ecoDTO;
  }
  public async Task<IEnumerable<EcosistemaDTO>> BuscarPorConservacion(string conservacion)
  {
    var ecosistemas = await repository.GetConservacion(Enum.Parse<Conservacion>(conservacion));

    if(ecosistemas == null) return Enumerable.Empty<EcosistemaDTO>();

    var ecoDTOs = ecosistemas.Select(e => new EcosistemaDTO
    {
      Id=e.Id,
      Descripcion=e.Descripcion,
      AreaLatitud=e.AreaLatitud,
      AreaLongitud=e.AreaLongitud,
      Conservacion=e.Conservacion.ToString()
    }).ToList();
    
    return ecoDTOs;
  }
}