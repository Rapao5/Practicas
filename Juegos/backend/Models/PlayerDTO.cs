namespace backend.playerDTO;

public class PlayerDTO
{
  public int Id {get; set;}
  public required string Nombre {get; set;}
  public required string Especialidad {get; set;}
  public int[] IdGames {get; set;}
}