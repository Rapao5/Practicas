using backend.ecoDTO;
using backend.investigador;
using backend.investigadorDTO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestigadorController : ControllerBase
{
  private readonly IInvestigadorService service;

  public InvestigadorController(IInvestigadorService service)
  {
    this.service = service;
  }

  [HttpGet("paginados")]
  public async Task<ActionResult<IEnumerable<InvestigadorDTO>>> GetInvestigadores([FromQuery] int skip = 0, [FromQuery] int take = 10)
  {
    var investigadores = await service.ObtenerTodosPaginadosAsync(skip, take);
    return Ok(investigadores);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<InvestigadorDTO>>> Get()
  {
    var lista = await service.ObtenerTodosAsync();
    return Ok(lista);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<InvestigadorDTO>> GetId(int id)
  {
    var inv = await service.ObtenerPorIdAsync(id);
    if(inv == null) return NotFound($"No existe un investigador con Id: {id}");
    return Ok(inv);
  } 

  [HttpPost]
  public async Task<ActionResult<InvestigadorDTO>> Post(InvestigadorDTO dto)
  {
      var resultado = await service.CrearAsync(dto);
      return CreatedAtAction(nameof(GetId), new {id = resultado.Id}, resultado);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Update(int id, InvestigadorDTO dto)
  {
    
    if(id != dto.Id) return BadRequest("Los Ids no coinciden.");
    await service.UpdateAsync(id, dto);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    await service.DeleteAsync(id);
    return NoContent();
  }

  [HttpGet("nombre/{nombre}")]
  public async Task<ActionResult<IEnumerable<InvestigadorDTO>>> Getnombre(string nombre)
  {
    var lista = await service.BuscarPorNombreAsync(nombre);
    return Ok(lista);
  }

  [HttpGet("experiencia/{experiencia}")]
  public async Task<ActionResult<IEnumerable<InvestigadorDTO>>> GetExperiencia(string experiencia)
  {
    var lista = await service.BuscarPorExperiencia(experiencia);
    return Ok(lista);
  }

  [HttpGet("proyecto/{id}")]
  public async Task<ActionResult<IEnumerable<InvestigadorDTO>>> GetPoyecto(int id)
  {
    var lista = await service.BuscarPorProyecto(id);
    return Ok(lista);
  }
}