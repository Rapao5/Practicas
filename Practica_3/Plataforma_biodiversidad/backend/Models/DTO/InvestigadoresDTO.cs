namespace backend.investigadorDTO;

public class InvestigadorDTO
{
  public int Id {get; set;}
  public string Nombre {get; set;} = null!;
  public string Email {get; set;} = null!;
  public string Experiencia {get; set;} = null!;
  public List<string> Asignaciones {get; set;} = new();
  public List<string> Proyectos {get; set;} = new();
}