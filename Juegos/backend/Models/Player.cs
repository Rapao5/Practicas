namespace backend.player;

public class Player
{
  public int Id {get; set;}
  public required string Nombre {get; set;}
  public required string Especialidad {get; set;}
  public int[] IdGames {get; set;}
}