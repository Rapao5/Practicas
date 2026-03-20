using backend.Data;
using backend.game;
using backend.gameDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesController; 

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
  private readonly AppDbContext context;

  public GamesController(AppDbContext context)
  {
    this.context=context;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Game>> GetGame(int id)
  {
    var game = await context.Games.FindAsync(id);
    if (game ==null)
    {
      return NotFound();
    }

    var gameDTO = new GameDTO
    {
      Id = game.Id,
      Titulo = game.Titulo,
      Genero = game.Genero,
      Descripcion = game.Descripcion,
      Jugado = game.Jugado,
      Tiempo = game.Tiempo,
      Puntuacion = game.Puntuacion
    };
    return Ok(gameDTO);

  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<GameDTO>>> filtrarGame(
    [FromQuery] string? titulo,
    [FromQuery] string? genero,
    [FromQuery] bool? jugado,
    [FromQuery] double? tiempo,
    [FromQuery] double? tiempoMayorA,
    [FromQuery] double? tiempoMenorA,
    [FromQuery] double? puntuacionMayorA,
    [FromQuery] double? puntuacionMenorA,
    [FromQuery] double? puntuacion)
  {
    var consulta = context.Games.AsQueryable();
    if(!string.IsNullOrEmpty(titulo)) consulta = consulta.Where( g => g.Titulo.Contains(titulo));
    if(!string.IsNullOrEmpty(genero)) consulta = consulta.Where( g => g.Genero.Contains(genero));
    if(jugado!=null) consulta = consulta.Where(g => g.Jugado == jugado);
    if(tiempo.HasValue) consulta = consulta.Where(g => g.Tiempo == tiempo);
    if(tiempoMayorA.HasValue) consulta = consulta.Where(g => g.Tiempo >= tiempoMayorA);
    if(tiempoMenorA.HasValue) consulta = consulta.Where(g => g.Tiempo <= tiempoMenorA);
    if(puntuacionMayorA.HasValue) consulta = consulta.Where(g => g.Puntuacion >= puntuacionMayorA);
    if(puntuacionMenorA.HasValue) consulta = consulta.Where(g => g.Puntuacion <= puntuacionMenorA);
    if(puntuacion.HasValue) consulta = consulta.Where(g => g.Puntuacion == puntuacion);

    var resultados = await consulta.Select( g => new GameDTO {

       Id=g.Id,
       Titulo = g.Titulo,
       Genero = g.Genero,
       Descripcion = g.Descripcion,
       Jugado = g.Jugado,
       Tiempo = g.Tiempo,
       Puntuacion = g.Puntuacion

    }).ToListAsync();

    return Ok(resultados);

  }

  [HttpPost]
  public async Task<ActionResult<Game>> CrearGame([FromBody] Game nuevoGame)
  {
    if (nuevoGame == null)
    {
      return BadRequest("Datos no válidos");
    }

    context.Games.Add(nuevoGame);
    await context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetGame), new {id = nuevoGame.Id}, nuevoGame);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<Game>> DeleteGame(int id)
  {
    var game = await context.Games.FindAsync(id);
    if(game == null) return NotFound($"No existe el game con id {id}");
    context.Games.Remove(game);
    await context.SaveChangesAsync();
    return NoContent();
  }

  [HttpPut]
  public async Task<ActionResult<Game>> UpdateGame([FromBody] Game updateGame)
  {
    var game = await context.Games.FindAsync(updateGame.Id);
    if(game == null) return NotFound($"No existe el game con ID {updateGame.Id}");
    
    game.Titulo = updateGame.Titulo;
    game.Genero = updateGame.Genero;
    game.Descripcion = updateGame.Descripcion;
    game.Jugado = updateGame.Jugado;
    game.Tiempo = updateGame.Tiempo;
    game.Puntuacion = updateGame.Puntuacion;

    await context.SaveChangesAsync();
    return Ok(game);
  }
}