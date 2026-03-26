using backend.Data;
using backend.game;
using backend.gameDTO;
using backend.player;
using backend.playerDTO;
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

  [HttpGet("playerGames")]
  public async Task<ActionResult> GetPlayerGames()
  {

    var allPlayers = await context.Players.ToListAsync();
    var allGames = await context.Games.ToListAsync();

    Dictionary<int, int> playerGames = new Dictionary<int, int>();
    var diccionarioGames = allGames.ToDictionary(g => g.Id);

    foreach (var player in allPlayers)
    {
      int cantidad = player.IdGames != null ? player.IdGames.Length : 0;
      playerGames.Add(player.Id, cantidad);
    }

    var respuesta = allPlayers.Select(p => new
    {
      Id = p.Id,
      Nombre = p.Nombre,
      Especialidad = p.Especialidad,
      Games = p.IdGames.Select(id =>
        diccionarioGames.GetValueOrDefault(id)
      )
    });

    return Ok(respuesta);
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

  [HttpGet("player/{id}")]
  public async Task<ActionResult<Game>> GetPlayer(int id)
  {
    var player = await context.Players.FindAsync(id);
    if (player ==null)
    {
      return NotFound();
    }

    var playerDTO = new PlayerDTO
    {
      Id = player.Id,
      Nombre = player.Nombre,
      Especialidad = player.Especialidad,
      IdGames = player.IdGames
    };
    return Ok(playerDTO);

  }
  
  [HttpPost("seedPlayer")]
  public async Task<IActionResult> anadirPlayers()
  {
    Random generador = new Random();
    int total = 1000;
    var lista = new List<Player>();
    for(int i = 0; i < total; i++)
    {
      lista.Add(new Player{
        Nombre = $"Nombre {i}",
        Especialidad = $"Especialidad {i}",
        IdGames =new int[] {generador.Next(1, 50001), generador.Next(1, 50001)}
      });
    }
    await context.Players.AddRangeAsync(lista);
    await context.SaveChangesAsync();
    return Ok("Base de datos poblada con 1000 jugadores");
  }

  [HttpPost("seedGame")]
  public async Task<IActionResult> anadirGames()
  {
    int total = 50000;
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

    return Ok("Base de datos poblada con 50.000 games.");
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
    [FromQuery] double? jugador,
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
    if(jugador!=null) consulta = consulta.Where(g => context.Players.Any( p => p.Id == jugador && p.IdGames.Contains(g.Id)));

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

  [HttpGet("paginado/player")]
  public async Task<ActionResult<IEnumerable<PlayerDTO>>> filtrarPlayer(
    [FromQuery] string? nombre,
    [FromQuery] string? especialidad,
    [FromQuery] int? game,
    [FromQuery] int pagina = 1,
    [FromQuery] int cantidad = 50)
  {
    var consulta = context.Players.AsQueryable();
    if(!string.IsNullOrEmpty(nombre)) consulta = consulta.Where( p => p.Nombre.ToLower().Contains(nombre.ToLower()));
    if(!string.IsNullOrEmpty(especialidad)) consulta = consulta.Where( p => p.Especialidad.ToLower().Contains(especialidad.ToLower()));
    if(game!=null) consulta = consulta.Where(p => p.IdGames.Contains(game.Value));

    var totalRegistros = await consulta.CountAsync();

    var jugadores = await consulta
    .OrderBy(p => p.Id)
    .Skip((pagina - 1)  * cantidad)
    .Take(cantidad)
    .ToListAsync();

    var idJuegos = jugadores
    .SelectMany(p => p.IdGames ?? Array.Empty<int>())
    .Distinct()
    .ToList();

    var diccionarioGames = await context.Games
    .Where(g => idJuegos.Contains(g.Id))
    .ToDictionaryAsync(g => g.Id);

    var resultados = jugadores.Select(p => new
    {
      p.Id,
      p.Nombre,
      p.Especialidad,
      Game = p.IdGames?.Select(id => diccionarioGames.GetValueOrDefault(id)).Where(game => game != null)
    });

    return Ok(new
    {
      Total = totalRegistros,
      Player = resultados
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

  [HttpPost("crearPlayer")]
  public async Task<ActionResult<Player>> CrearPlayer([FromBody] Player nuevoPlayer)
  {
    if (nuevoPlayer == null)
    {
      return BadRequest("Datos no válidos");
    }

    var comprobarID = nuevoPlayer.IdGames ?? Array.Empty<int>();

    var existe = await context.Games
    .Where(g => comprobarID.Contains(g.Id))
    .CountAsync();

    if (existe != comprobarID.Length)
    {
      return BadRequest("Uno o más Ids de juegos no son válidos.");
    }

    context.Players.Add(nuevoPlayer);
    await context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetPlayer), new {id = nuevoPlayer.Id}, nuevoPlayer);
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

  [HttpDelete("deletePlayer/{id}")]
  public async Task<ActionResult<Player>> DeletePlayer(int id)
  {
    var player = await context.Players.FindAsync(id);
    if(player == null) return NotFound($"No existe el player con id {id}");
    context.Players.Remove(player);
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

  [HttpPut("updatePlayer")]
  public async Task<ActionResult<Player>> UpdatePlayer([FromBody] Player updatePlayer)
  {
    var player = await context.Players.FindAsync(updatePlayer.Id);
    if(player == null) return NotFound($"No existe el game con ID {updatePlayer.Id}");
    
    player.Nombre = updatePlayer.Nombre;
    player.Especialidad = updatePlayer.Especialidad;
    player.IdGames = updatePlayer.IdGames;

    await context.SaveChangesAsync();
    return Ok(player);
  }
}