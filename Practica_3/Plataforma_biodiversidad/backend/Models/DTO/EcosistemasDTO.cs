namespace backend.ecoDTO;

public class EcosistemaDTO
{
  public int Id {get; set;}
  public string Descripcion {get; set;} = null!;
  public required decimal AreaLatitud {get; set;}
  public required decimal AreaLongitud {get; set;}
  public string Conservacion {get; set;} = null!;
  
}