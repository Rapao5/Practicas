using backend.eco;
using backend.ecoDTO;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EcosistemaController : ControllerBase
{
  private readonly IEcosistemaRepository repository;

  public EcosistemaController(IEcosistemaRepository repository)
  {
    this.repository = repository;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<EcosistemaDTO>>> Get()
  {
    var ecosistemas = await repository.GetEcosistemaAsync();

    var ecosistemasDTO = ecosistemas.Select(e => new EcosistemaDTO
    {
      Id = e.Id,
      Descripcion = e.Descripcion,
      AreaLatitud = e.AreaLatitud,
      AreaLongitud = e.AreaLongitud,
      Conservacion = e.Conservacion.ToString()
    });

    return Ok(ecosistemasDTO);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<EcosistemaDTO>> GetId(int id)
  {
    var ecosistema = await repository.GetByIdAsync(id);

    if(ecosistema == null) return NotFound($"El ecosistema con Id: {id} no existe.");

    var ecosistemaDTO =new EcosistemaDTO
    {
      Id = ecosistema.Id,
      Descripcion = ecosistema.Descripcion,
      AreaLatitud = ecosistema.AreaLatitud,
      AreaLongitud = ecosistema.AreaLongitud,
      Conservacion = ecosistema.Conservacion.ToString()
    };

    return Ok(ecosistemaDTO);
  }

  [HttpPost]
  public async Task<ActionResult<EcosistemaDTO>> Post(EcosistemaDTO dto)
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
    return CreatedAtAction(nameof(GetId), new { id = dto.Id }, dto);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<EcosistemaDTO>> Update(int id, EcosistemaDTO dto)
  {

    if (id != dto.Id) return BadRequest($"El Id: {id} no coincide con el ecosistema.");

    var eco = await repository.GetByIdAsync(id);
    if(eco == null ) return NotFound($"El ecosistema con ID: {id} no existe.");

      eco.Descripcion = dto.Descripcion;
      eco.AreaLatitud = dto.AreaLatitud;
      eco.AreaLongitud = dto.AreaLongitud;
      eco.Conservacion = Enum.Parse<Conservacion>(dto.Conservacion);

    await repository.Update(eco);

    var ecoDTO = new EcosistemaDTO
    {
      Id=eco.Id,
      Descripcion=eco.Descripcion,
      AreaLatitud=eco.AreaLatitud,
      AreaLongitud=eco.AreaLongitud,
      Conservacion=eco.Conservacion.ToString()
    };

    return Ok(ecoDTO);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    var ecosistema = await repository.GetByIdAsync(id);

    if(ecosistema == null) return NotFound($"El ecosistema con Id: {id} no existe.");

    await repository.Delete(id);
    return NoContent();
  }


  [HttpGet("area/{areaLatitud}/{areaLongitud}")]
  public async Task<ActionResult<EcosistemaDTO>> GetArea(decimal areaLatitud, decimal areaLongitud)
  {
    var ecosistemas = await repository.GetAreaEcosistema(areaLatitud, areaLongitud);

    if(ecosistemas == null) return BadRequest("No existe un ecosistema con esas coordeanadas.");

    var ecoDTO = new EcosistemaDTO
    {
      Id=ecosistemas.Id,
      Descripcion=ecosistemas.Descripcion,
      AreaLatitud=ecosistemas.AreaLatitud,
      AreaLongitud=ecosistemas.AreaLongitud,
      Conservacion=ecosistemas.Conservacion.ToString()
    };

    return Ok(ecoDTO);
  }

  [HttpGet("conservacion/{conservacion}")]
  public async Task<ActionResult<IEnumerable<EcosistemaDTO>>> GetConservacion(string conservacion)
  {
    var ecosistemas = await repository.GetConservacion(Enum.Parse<Conservacion>(conservacion));

    if(ecosistemas == null) return BadRequest("No existe un ecosistema con esa conservación.");

    var ecoDTOs = ecosistemas.Select(e => new EcosistemaDTO
    {
      Id=e.Id,
      Descripcion=e.Descripcion,
      AreaLatitud=e.AreaLatitud,
      AreaLongitud=e.AreaLongitud,
      Conservacion=e.Conservacion.ToString()
    });
    
    return Ok(ecoDTOs);
  }
}