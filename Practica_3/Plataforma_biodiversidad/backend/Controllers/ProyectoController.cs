using backend.proyectoDTO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectoController : ControllerBase
{
  private readonly IProyectoService service;

  public ProyectoController(IProyectoService service)
  {
    this.service=service;
  }
  [HttpGet("paginados")]
  public async Task<ActionResult<IEnumerable<ProyectoDTO>>> GetProyectos([FromQuery] int skip = 0, [FromQuery] int take = 10)
  {
      var proyectos = await service.ObtenerTodosPaginadosAsync(skip, take);
      return Ok(proyectos);
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ProyectoDTO>>> Get()
  {
    var lista = await service.ObtenerTodosAsync();
    return Ok(lista);
  }
  
  [HttpGet("{id}")]
  public async Task<ActionResult<ProyectoDTO>> GetId(int id)
  {
    var pro = await service.ObtenerPorIdAsync(id);
    if(pro == null) return NotFound($"No existe un proyecto con Id: {id}");
    return Ok(pro);
  }

  [HttpPost]
  public async Task<ActionResult<ProyectoDTO>> Post(ProyectoDTO dto)
  {
    try
    {
      var nuevoPro = await service.CrearAsync(dto);
      return CreatedAtAction(nameof(GetId), new {id = nuevoPro.Id}, nuevoPro);
    }
    catch(ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
  }
  
  [HttpPut("{id}")]
  public async Task<ActionResult> Update(int id, ProyectoDTO dto)
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

  [HttpGet("especieFoco/{especieFoco}")]
  public async Task<ActionResult<IEnumerable<ProyectoDTO>>> GetProyectoByEspecieFoco(string especieFoco)
  {
    var lista = await service.BuscarPorEspecieFoco(especieFoco);
    return Ok(lista);
  }

  [HttpGet("estado/{estado}")]
  public async Task<ActionResult<IEnumerable<ProyectoDTO>>> GetProyectoByEstado(bool estado)
  {
    var lista = await service.BuscarPorEstado(estado);
    return Ok(lista);
  }

  [HttpGet("ecosistema/{id}")]
  public async Task<ActionResult<IEnumerable<ProyectoDTO>>> GetProyectoByEcosistema(int id)
  {
    var lista = await service.BuscarPorEcosistema(id);
    return Ok(lista);
  }
}