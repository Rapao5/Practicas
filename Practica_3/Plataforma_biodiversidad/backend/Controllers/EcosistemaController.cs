using backend.ecoDTO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EcosistemaController : ControllerBase
{
  private readonly IEcosistemaService service;

  public EcosistemaController(IEcosistemaService service)
  {
    this.service = service;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<EcosistemaDTO>>> Get()
  {
    var lista = await service.ObtenerTodosAsync();
    return Ok(lista);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<EcosistemaDTO>> GetId(int id)
  {
    var eco = await service.ObtenerPorIdAsync(id);
    if(eco == null) return NotFound($"No existe un ecosistema con Id: {id}");
    return Ok(eco);
  }

  [HttpPost]
  public async Task<ActionResult<EcosistemaDTO>> Post(EcosistemaDTO dto)
  {
    var resultado = await service.CrearAsync(dto);
      return CreatedAtAction(nameof(GetId), new {id = resultado.Id}, resultado);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<EcosistemaDTO>> Update(int id, EcosistemaDTO dto)
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


  [HttpGet("area/{areaLatitud}/{areaLongitud}")]
  public async Task<ActionResult<EcosistemaDTO>> GetArea(decimal areaLatitud, decimal areaLongitud)
  {
    var lista = await service.BuscarPorArea(areaLatitud, areaLongitud);
    return Ok(lista);
  }

  [HttpGet("conservacion/{conservacion}")]
  public async Task<ActionResult<IEnumerable<EcosistemaDTO>>> GetConservacion(string conservacion)
  {
    var lista = await service.BuscarPorConservacion(conservacion);
    return Ok(lista);
  }
}