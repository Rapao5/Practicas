using backend.Interfaces;
using backend.proyecto;
using backend.proyectoDTO;

namespace backend.Services;

public class ProyectoService : IProyectoService
{
  private readonly IProyectoRepository repository;

  public ProyectoService(IProyectoRepository repository)
  {
    this.repository = repository;
  }

  public async Task<IEnumerable<ProyectoDTO>> ObtenerTodosAsync()
  {
    var proyectos = await repository.GetProyectosAsync();

    var proyectoDTO = proyectos.Select(p => new ProyectoDTO
    {
      Id = p.Id,
      Nombre = p.Nombre,
      Presupuesto = p.Presupuesto,
      FechaInicio = p.FechaInicio,
      FechaFinal = p.FechaFinal,
      Estado = p.Estado,
      EspecieFoco = p.EspecieFoco,
      Ecosistema = p.Ecosistema?.Descripcion,
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList()
    }).ToList();
    
    return proyectoDTO;
  }

  public async Task<ProyectoDTO> ObtenerPorIdAsync(int id)
  {
    var proyecto = await repository.GetByIdAsync(id);

    if(proyecto == null) return null;

    var proyectoDTO = new ProyectoDTO
    {
      Id = proyecto .Id,
      Nombre = proyecto .Nombre,
      Presupuesto = proyecto .Presupuesto,
      FechaInicio = proyecto .FechaInicio,
      FechaFinal = proyecto .FechaFinal,
      Estado = proyecto .Estado,
      EspecieFoco = proyecto .EspecieFoco,
      Ecosistema = proyecto .Ecosistema?.Descripcion,
      Investigadores = proyecto .Asignaciones.Select(a => a.Investigador.Nombre).ToList()
    };
    return proyectoDTO;
  }

  public async Task<ProyectoDTO> CrearAsync(ProyectoDTO dto)
  {
    var nuevoProyecto = new Proyecto
    {
      Nombre = dto.Nombre,
      Presupuesto = dto.Presupuesto,
      FechaInicio = dto.FechaInicio,
      FechaFinal = dto.FechaFinal,
      Estado = dto.Estado,
      EspecieFoco = dto.EspecieFoco,
      EcosistemaId = dto.EcosistemaId
    };

    await repository.AddAsync(nuevoProyecto);
    dto.Id = nuevoProyecto.Id;
    return dto;
  }

  public async Task UpdateAsync(int id, ProyectoDTO dto)
  {
    if (id != dto.Id) return;

    var nuevoProyecto = await repository.GetByIdAsync(id);
    if(nuevoProyecto==null) return;

    nuevoProyecto.Nombre=dto.Nombre;
    nuevoProyecto.Presupuesto=dto.Presupuesto;
    nuevoProyecto.FechaInicio=dto.FechaInicio;
    nuevoProyecto.FechaFinal=dto.FechaFinal;
    nuevoProyecto.Estado=dto.Estado;
    nuevoProyecto.EspecieFoco=dto.EspecieFoco;
    nuevoProyecto.EcosistemaId=dto.EcosistemaId;

    await repository.Update(nuevoProyecto);
  }

  public async Task DeleteAsync(int id)
  {
    var proyecto = await repository.GetByIdAsync(id);
    
    if(proyecto == null) return;

    await repository.Delete(id);
  }

  public async Task<IEnumerable<ProyectoDTO>> BuscarPorEspecieFoco(string especieFoco)
  {
    var proyecto = await repository.GetProyectoByEspecieFoco(especieFoco);
    if(proyecto == null) return null;
    var proyectoDTO = proyecto.Select(p => new ProyectoDTO
    {
      Id = p.Id,
      Nombre = p.Nombre,
      Presupuesto = p.Presupuesto,
      FechaInicio = p.FechaInicio,
      FechaFinal = p.FechaFinal,
      Estado = p.Estado,
      EspecieFoco = p.EspecieFoco,
      Ecosistema = p.Ecosistema?.Descripcion,
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList()
    }).ToList();

    return proyectoDTO;
  }

  public async Task<IEnumerable<ProyectoDTO>> BuscarPorEstado(bool estado)
  {
    var proyectos = await repository.GetProyectoByEstado(estado);

    if(proyectos == null) return Enumerable.Empty<ProyectoDTO>();

    var proyectoDTO = proyectos.Select(p => new ProyectoDTO
    {
      Id = p.Id,
      Nombre = p.Nombre,
      Presupuesto = p.Presupuesto,
      FechaInicio = p.FechaInicio,
      FechaFinal = p.FechaFinal,
      Estado = p.Estado,
      EspecieFoco = p.EspecieFoco,
      Ecosistema = p.Ecosistema?.Descripcion,
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList()
    }).ToList();
    
    return proyectoDTO;
  }

  public async Task<IEnumerable<ProyectoDTO>> BuscarPorEcosistema(int ecosistemaId)
  {
    var proyectos = await repository.GetProyectoByEcosistema(ecosistemaId);
    if(proyectos == null) return Enumerable.Empty<ProyectoDTO>();
    var proyectoDTO = proyectos.Select(p => new ProyectoDTO
    {
      Id = p.Id,
      Nombre = p.Nombre,
      Presupuesto = p.Presupuesto,
      FechaInicio = p.FechaInicio,
      FechaFinal = p.FechaFinal,
      Estado = p.Estado,
      EspecieFoco = p.EspecieFoco,
      Ecosistema = p.Ecosistema?.Descripcion,
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList()
    }).ToList();

    return proyectoDTO;
  }
}