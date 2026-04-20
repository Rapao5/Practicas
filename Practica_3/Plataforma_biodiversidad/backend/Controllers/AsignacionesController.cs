using backend.asignaciones;
using backend.asignacionesDTO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsignacionesController : ControllerBase
{
  private readonly IAsignacionesService service;

  public AsignacionesController(IAsignacionesService service)
  {
    this.service = service;
  }
  
  [HttpGet]
  public async Task<ActionResult<IEnumerable<AsignacionesDTO>>> Get()
  {
    var lista = await service.ObtenerTodosAsync();
    return Ok(lista);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<AsignacionesDTO>> GetById(int id)
  {
    var asignacion = await service.ObtenerPorIdAsync(id);
    if(asignacion == null) return NotFound($"No existe una asignacion con Id: {id}");
    return Ok(asignacion);
  }

  [HttpPost]
  public async Task<ActionResult<AsignacionesDTO>> AddAsync(AsignacionesDTO dto)
  {
    try
    {
      var nuevaAsignacion = await service.CrearAsync(dto);
    return CreatedAtAction(nameof(GetById), new {id = nuevaAsignacion.Id}, nuevaAsignacion);
    }
    catch (ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Update(int id, AsignacionesDTO dto)
  {
    try
    {
      if(id != dto.Id) return BadRequest("Los Ids no coinciden.");
      await service.UpdateAsync(id, dto);
      return NoContent();
    }
    catch (ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    await service.DeleteAsync(id);
    return NoContent();
  }

  [HttpGet("proyecto/{proyectoId}")]
  public async Task<ActionResult<IEnumerable<AsignacionesDTO>>> GetAsignacionesByProyecto(int proyectoId)
  {
    var lista =  await service.MostrarPorProyecto(proyectoId);
    return Ok(lista);
  }

  [HttpGet("rol/{rol}")]
  public async Task<ActionResult<IEnumerable<AsignacionesDTO>>> GetRol(Rol rol)
  {
    var lista = await service.BuscarPorRolAsync(rol);
    return Ok(lista);
  }

  [HttpGet("fecha/{fecha}")]
  public async Task<ActionResult<IEnumerable<AsignacionesDTO>>> GetFechaEntrada(DateOnly fecha)
  {
    var lista = await service.BuscarPorFechaEntrada(fecha);
    return Ok(lista);
  }
}