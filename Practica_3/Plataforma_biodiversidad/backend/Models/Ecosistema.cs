using System.ComponentModel.DataAnnotations;
using backend.proyecto;

namespace backend.eco;

public enum Conservacion
  {
    Intacto,
    Vulnerable,
    EnPeligro,
    EnRestauracion,
    Protegido
  }
public class Ecosistema  
{
  public int Id {get; set;}
  [Required]
  [MaxLength(200)]
  public string Nombre {get; set;} = null!;
  [Required]
  [MaxLength(500)]
  public string Descripcion {get; set;} = null!;
  public required decimal Area {get; set;}
  public required decimal AreaLatitud {get; set;}
  public required decimal AreaLongitud {get; set;}
  public Conservacion Conservacion {get; set;}
  public ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}