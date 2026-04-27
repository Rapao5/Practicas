using System.ComponentModel.DataAnnotations;
using backend.asignaciones;
using backend.eco;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace backend.proyecto;

public class Proyecto
{
  public int Id {get; set;}
  [Required]
  [MinLength(3, ErrorMessage = "El nombre es demasiado corto")]
  [MaxLength(100)]
  public string Nombre {get; set;} = null!;
  public required decimal Presupuesto {get; set;}
  public required DateOnly FechaInicio {get; set;}
  public required DateOnly FechaFinal {get; set;}
  public required bool Estado {get; set;}
  public required string EspecieFoco {get; set;}
  public int EcosistemaId {get; set;}
  public Ecosistema Ecosistema {get; set;}
  public ICollection<Asignaciones> Asignaciones {get; set;} = new List<Asignaciones>();
}