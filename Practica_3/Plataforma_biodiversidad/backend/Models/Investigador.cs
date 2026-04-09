using System.ComponentModel.DataAnnotations;
using backend.asignaciones;
using backend.proyecto;

namespace backend.investigador;

public enum Experiencia
  {
    Junior,
    Semi_Senior,
    Senior
  }
public class Investigador
{
  public int Id {get; set;}
  [Required]
  [MaxLength(200)]
  public string Nombre {get; set;} = null!;
  [Required]
  [MaxLength(500)]
  public required string Email {get; set;}
  public Experiencia Experiencia {get; set;}
  public ICollection<Proyecto> Proyectos = new List<Proyecto>();
  public ICollection<Asignaciones> Asignaciones { get; set; } = new List<Asignaciones>();
}