using backend.Interfaces;
using backend.proyecto;
using backend.proyectoDTO;

namespace backend.Services;

public class ProyectoService : IProyectoService
{
  private readonly IProyectoRepository repository;
  private readonly IEcosistemaRepository ecosistemaRepository;

  public ProyectoService(IProyectoRepository repository, IEcosistemaRepository ecosistemaRepository)
  {
    this.repository = repository;
    this.ecosistemaRepository = ecosistemaRepository;
  }

  public async Task<IEnumerable<ProyectoDTO>> ObtenerTodosPaginadosAsync(int skip, int take)
  {
    var proyectos = await repository.GetProyectosPaginadosAsync(skip, take);

    return proyectos.Select(p => new ProyectoDTO
    {
      Id = p.Id,
      Nombre = p.Nombre,
      Presupuesto = p.Presupuesto,
      FechaInicio = p.FechaInicio,
      FechaFinal = p.FechaFinal,
      Estado = p.Estado,
      EspecieFoco = p.EspecieFoco,
      Ecosistema = p.Ecosistema?.Descripcion,
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList(),
      InvestigadoresRol = p.Asignaciones.Select(a => a.Rol.ToString()).ToList()
    }).ToList();
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
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList(),
      InvestigadoresRol = p.Asignaciones.Select(a => a.Rol.ToString()).ToList()
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
      EcosistemaId = proyecto.EcosistemaId,
      Ecosistema = proyecto .Ecosistema?.Descripcion,
      Investigadores = proyecto .Asignaciones.Select(a => a.Investigador.Nombre).ToList(),
      InvestigadoresRol = proyecto.Asignaciones.Select(a => a.Rol.ToString()).ToList()
    };
    return proyectoDTO;
  }

  public async Task<ProyectoDTO> CrearAsync(ProyectoDTO dto)
  {
    var ecosistema = await ecosistemaRepository.GetByIdBasicAsync(dto.EcosistemaId);
    if(dto.Presupuesto < 0) throw new ArgumentException("El presupuesto del proyecto es menor a 0.");
    if(dto.FechaFinal < dto.FechaInicio) throw new ArgumentException("La fecha final no puede ser anterior a la inicial (No es una máquina de tiempo).");
    if(ecosistema == null) throw new ArgumentException("El ecosistema no existe.");
    if(ecosistema.Conservacion==0) throw new ArgumentException("No se puede asignar un proyecto en un ecosistema intacto");

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
    var ecosistema = await ecosistemaRepository.GetByIdAsync(dto.EcosistemaId);
    if(dto.Presupuesto < 0) throw new ArgumentException("El presupuesto del proyecto es menor a 0.");
    if(dto.FechaFinal < dto.FechaInicio) throw new ArgumentException("La fecha final no puede ser anterior a la inicial (No es una máquina de tiempo).");
    if(ecosistema == null) throw new ArgumentException("El ecosistema no existe.");
    if(ecosistema.Conservacion==0) throw new ArgumentException("No se puede asignar un proyecto en un ecosistema intacto");
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
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList(),
      InvestigadoresRol = p.Asignaciones.Select(a => a.Rol.ToString()).ToList()
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
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList(),
      InvestigadoresRol = p.Asignaciones.Select(a => a.Rol.ToString()).ToList()
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
      Investigadores = p.Asignaciones.Select(a => a.Investigador.Nombre).ToList(),
      InvestigadoresRol = p.Asignaciones.Select(a => a.Rol.ToString()).ToList()
    }).ToList();

    return proyectoDTO;
  }
}