using System.ComponentModel.DataAnnotations;
using backend.proyecto;

namespace Biodiversidad.Tests.Models
{

  // prueba de validaciones
  public class ProyectoValidacionTest
  {
    [Theory]
    [InlineData("Proyecto Biodiversidad", true)]
    [InlineData("Ab", false)]
    [InlineData("Abc", true)]
    public void NombreProyecto(string nombreInvalido, bool resultadoEsperado)
    {
      var proyecto = new Proyecto {Nombre = nombreInvalido,
                                    Presupuesto = 0,
                                    FechaInicio = DateOnly.FromDateTime(DateTime.Now),
                                    FechaFinal = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                                    Estado = false,
                                    EspecieFoco = ""  
                                    };
      var contexto = new ValidationContext(proyecto);
      var resultados = new List<ValidationResult>();
      bool esValido = Validator.TryValidateObject(proyecto, contexto, resultados, true);
      Assert.Equal(resultadoEsperado, esValido);
    }
  }
}