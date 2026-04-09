namespace backend.proyectoDTO;

public class ProyectoDTO
{
  public int Id {get; set;}
  public string Nombre {get; set;} = null!;
  public decimal Presupuesto {get; set;}
  public DateOnly FechaInicio {get; set;}
  public DateOnly FechaFinal {get; set;}
  public bool Estado {get; set;}
  public string EspecieFoco {get; set;} = null!;
  public int EcosistemaId {get; set;}
  public string Ecosistema {get; set;} = null!;
  public List<string> Investigadores {get; set;} = new();
}