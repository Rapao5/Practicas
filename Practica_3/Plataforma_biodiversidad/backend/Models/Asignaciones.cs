using System.ComponentModel.DataAnnotations;
using backend.investigador;
using backend.proyecto;

namespace backend.asignaciones;

public class Asignaciones
{
  public int Id {get; set;}
  [Required]
  [MaxLength(50)]
  public string Rol {get; set;} = null!;
  public required DateOnly FechaEntrada {get; set;}
  public int ProyectoId { get; set; }
  public int InvestigadorId { get; set; }
  public Proyecto Proyecto {get; set;}
  public Investigador Investigador {get; set;}
}