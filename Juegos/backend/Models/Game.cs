namespace backend.game;

public class Game
{
  public int Id{get; set;}
  public required string Titulo{get; set;}
  public required string Genero{get; set;}
  public required string Descripcion{get; set;}
  public required bool Jugado {get; set;}
  public required double Tiempo {get; set;}
  public required double Puntuacion{get; set;}
  public required string Imagen {get; set;}
  public required int IdPlayer {get; set;}
}