namespace backend.ecoDTO;

public class EcosistemaDTO
{
  public int Id {get; set;}
  public string Nombre {get; set;} = null!;
  public string Descripcion {get; set;} = null!;
  public decimal Area {get; set;}
  public required decimal AreaLatitud {get; set;}
  public required decimal AreaLongitud {get; set;}
  public string Conservacion {get; set;} = null!;
  public List<string> Proyectos { get; set; } = new List<string>();
}