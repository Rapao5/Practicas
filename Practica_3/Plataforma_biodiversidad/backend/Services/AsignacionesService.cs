using backend.asignaciones;
using backend.asignacionesDTO;
using backend.Interfaces;

namespace backend.Services;

public class AsignacionesService : IAsignacionesService
{
  private readonly IAsignacionesRepository repository;
  private readonly IInvestigadorRepository investigadorRepository;
  private readonly IProyectoRepository proyectoRepository;
  public AsignacionesService(IAsignacionesRepository repository, IInvestigadorRepository investigadorRepository, IProyectoRepository proyectoRepository)
  {
    this.repository=repository;
    this.investigadorRepository=investigadorRepository;
    this.proyectoRepository=proyectoRepository;
  }

  public async Task<IEnumerable<AsignacionesDTO>> ObtenerTodosAsync()
  {
    var asignaciones = await repository.GetAsignacionesAsync();

    var asignacionesDTO = asignaciones.Select(a => new AsignacionesDTO
    {
      Id = a.Id,
      Rol = a.Rol.ToString(),
      FechaEntrada = a.FechaEntrada,
      ProyectoId = a.ProyectoId,
      InvestigadorId = a.InvestigadorId,
      Proyecto = a.Proyecto?.Nombre,
      Investigador = a.Investigador?.Nombre
    }).ToList();

    return asignacionesDTO;
  }

  public async Task<AsignacionesDTO> ObtenerPorIdAsync(int id)
  {
    var asignacion = await repository.GetByIdAsync(id);

    if(asignacion == null) return null;

    var asignacionDTO = new AsignacionesDTO
    {
      Id = asignacion.Id,
      Rol = asignacion.Rol.ToString(),
      FechaEntrada = asignacion.FechaEntrada,
      ProyectoId = asignacion.ProyectoId,
      InvestigadorId = asignacion.InvestigadorId,
      Proyecto = asignacion.Proyecto?.Nombre,
      Investigador = asignacion.Investigador?.Nombre
    };

    return asignacionDTO;
  }

  public async Task<AsignacionesDTO> CrearAsync(AsignacionesDTO dto)
  {
    var investigador = await investigadorRepository.GetByIdAsync(dto.InvestigadorId);
    if(investigador == null) throw new ArgumentException("El investigador no existe.");
    var asignaciones = await repository.GetAsignacionesAsync();
    foreach (var asig in asignaciones)
    {
      if(investigador.Id == asig.InvestigadorId)
      {
        var proyecto = await proyectoRepository.GetByIdAsync(asig.ProyectoId);
        if(proyecto.Estado == true)
        {
          if (asig.ProyectoId == dto.ProyectoId)
          {
            throw new ArgumentException("El investigador ya está asignado en este proyecto");
          }
          if(asig.Rol == 0 && dto.ProyectoId == asig.ProyectoId)
        {
          throw new ArgumentException("El investigador ya es lider de campo en otro proyecto");
        }
        }
      }
    }
    var asignacion = new Asignaciones
    {
      Rol = Enum.Parse<Rol>(dto.Rol),
      FechaEntrada = dto.FechaEntrada,
      ProyectoId = dto.ProyectoId,
      InvestigadorId = dto.InvestigadorId
    };

    await repository.AddAsync(asignacion);
    dto.Id = asignacion.Id;
    return dto;
  }

  public async Task UpdateAsync(int id, AsignacionesDTO dto)
  {
    if(id != dto.Id) return;
    var investigador = await investigadorRepository.GetByIdAsync(dto.InvestigadorId);
    if(investigador == null) throw new ArgumentException("El investigador no existe.");
    var asignaciones = await repository.GetAsignacionesAsync();
    foreach (var asig in asignaciones)
    {
      if(investigador.Id == asig.InvestigadorId && dto.Rol == "LiderCampo")
      {
        var proyecto = await proyectoRepository.GetByIdAsync(asig.ProyectoId);
        if(proyecto.Estado == true)
        {
          if(asig.Rol == 0 && dto.ProyectoId == asig.ProyectoId)
        {
          throw new ArgumentException("El investigador ya es lider de campo en otro proyecto");
        }
        }
      }
    }
    var nuevaAsignacion = await repository.GetByIdAsync(dto.Id);
    if(nuevaAsignacion == null) return;

      nuevaAsignacion.Id = dto.Id;
      nuevaAsignacion.Rol = Enum.Parse<Rol>(dto.Rol);
      nuevaAsignacion.FechaEntrada= dto.FechaEntrada;
      nuevaAsignacion.ProyectoId = dto.ProyectoId;
      nuevaAsignacion.InvestigadorId = dto.InvestigadorId;

    await repository.Update(nuevaAsignacion);
  }

  public async Task DeleteAsync(int id)
  {
    var asignacion = await repository.GetByIdAsync(id);
    
    if(asignacion == null) return;

    await repository.Delete(id);
  }

  public async Task<IEnumerable<AsignacionesDTO>> MostrarPorProyecto(int proyectoId)
  {
    var asignaciones = await repository.ObtenerPorProyectoAsync(proyectoId);

    if(asignaciones == null ) return Enumerable.Empty<AsignacionesDTO>();

     var asignacionesDTO = asignaciones.Select(a => new AsignacionesDTO
    {
      Id = a.Id,
      Rol = a.Rol.ToString(),
      FechaEntrada = a.FechaEntrada,
      ProyectoId = a.ProyectoId,
      InvestigadorId = a.InvestigadorId,
      Proyecto = a.Proyecto?.Nombre,
      Investigador = a.Investigador?.Nombre
    }).ToList();

    return asignacionesDTO;
  }

  public async Task<IEnumerable<AsignacionesDTO>> BuscarPorRolAsync(Rol rol)
  {
    var asignaciones = await repository.GetRol(rol);
    
    if(asignaciones == null) return Enumerable.Empty<AsignacionesDTO>();

    var asignacionesDTO = asignaciones.Select(a => new AsignacionesDTO
    {
      Id = a.Id,
      Rol = a.Rol.ToString(),
      FechaEntrada = a.FechaEntrada,
      ProyectoId = a.ProyectoId,
      InvestigadorId = a.InvestigadorId,
      Proyecto = a.Proyecto?.Nombre,
      Investigador = a.Investigador?.Nombre
    }).ToList();

    return asignacionesDTO;
  }

  public async Task<IEnumerable<AsignacionesDTO>> BuscarPorFechaEntrada(DateOnly fecha)
  {
    var asignaciones = await repository.GetFechaEntrada(fecha);
    
    if(asignaciones == null) return Enumerable.Empty<AsignacionesDTO>();

    var asignacionesDTO = asignaciones.Select(a => new AsignacionesDTO
    {
      Id = a.Id,
      Rol = a.Rol.ToString(),
      FechaEntrada = a.FechaEntrada,
      ProyectoId = a.ProyectoId,
      InvestigadorId = a.InvestigadorId,
      Proyecto = a.Proyecto?.Nombre,
      Investigador = a.Investigador?.Nombre
    }).ToList();

    return asignacionesDTO;
  }
}