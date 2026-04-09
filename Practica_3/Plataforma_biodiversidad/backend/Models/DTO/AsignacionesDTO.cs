namespace backend.asignacionesDTO;

public class AsignacionesDTO
{
  public int Id {get; set;}
  public string Rol {get; set;} = null!;
  public DateOnly FechaEntrada {get; set;}
  public int ProyectoId { get; set; }
  public int InvestigadorId { get; set; }
  public string Proyecto {get; set;} = null!;
  public string Investigador {get; set;} = null!;
}