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
      Puntuacion = game.Puntuacion,
      Imagen = game.Imagen
    };
    return Ok(gameDTO);

  }

  [HttpPost("seed")]
  public async Task<IActionResult> anadirGames()
  {

    int total = 100000;
    int loteSize = 5000;

    for (int i = 0; i < total; i += loteSize)
    {
        var lista = new List<Game>();
        for (int j = 1; j <= loteSize; j++)
        {
            lista.Add(new Game {
                Titulo = $"Juego Test {i + j}",
                Genero = "Simulación",
                Descripcion = "Generado masivamente",
                Imagen = "https://i.redd.it/czk30lrobkxa1.jpeg",
                Puntuacion = 5,
                Tiempo = 10,
                Jugado = false
            });
        }
        
        context.Games.AddRange(lista);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();
    }

    return Ok("Base de datos poblada con 100.000 registros.");
  }

  [HttpGet("paginado")]
  public async Task<ActionResult<IEnumerable<GameDTO>>> filtrarGame(
    [FromQuery] string? titulo,
    [FromQuery] string? genero,
    [FromQuery] bool? jugado,
    [FromQuery] double? tiempo,
    [FromQuery] double? tiempoMayorA,
    [FromQuery] double? tiempoMenorA,
    [FromQuery] double? puntuacionMayorA,
    [FromQuery] double? puntuacionMenorA,
    [FromQuery] double? puntuacion,
    [FromQuery] int pagina = 1,
    [FromQuery] int cantidad = 50)
  {
    var consulta = context.Games.AsQueryable();
    if(!string.IsNullOrEmpty(titulo)) consulta = consulta.Where( g => g.Titulo.ToLower().Contains(titulo.ToLower()));
    if(!string.IsNullOrEmpty(genero)) consulta = consulta.Where( g => g.Genero.ToLower().Contains(genero.ToLower()));
    if(jugado!=null) consulta = consulta.Where(g => g.Jugado == jugado);
    if(tiempo.HasValue) consulta = consulta.Where(g => g.Tiempo == tiempo);
    if(tiempoMayorA.HasValue) consulta = consulta.Where(g => g.Tiempo >= tiempoMayorA);
    if(tiempoMenorA.HasValue) consulta = consulta.Where(g => g.Tiempo <= tiempoMenorA);
    if(puntuacionMayorA.HasValue) consulta = consulta.Where(g => g.Puntuacion >= puntuacionMayorA);
    if(puntuacionMenorA.HasValue) consulta = consulta.Where(g => g.Puntuacion <= puntuacionMenorA);
    if(puntuacion.HasValue) consulta = consulta.Where(g => g.Puntuacion == puntuacion);

    var totalRegistros = await consulta.CountAsync();

    var resultados = await consulta
    .OrderBy(g => g.Id)
    .Skip((pagina - 1)  * cantidad)
    .Take(cantidad)
    .Select( g => new GameDTO {

       Id=g.Id,
       Titulo = g.Titulo,
       Genero = g.Genero,
       Descripcion = g.Descripcion,
       Jugado = g.Jugado,
       Tiempo = g.Tiempo,
       Puntuacion = g.Puntuacion,
       Imagen = g.Imagen

    }).ToListAsync();

    return Ok(new
    {
      Total = totalRegistros,
      Juegos = resultados
    });

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
    game.Imagen = updateGame.Imagen;

    await context.SaveChangesAsync();
    return Ok(game);
  }
}