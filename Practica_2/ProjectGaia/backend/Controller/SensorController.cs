using Microsoft.AspNetCore.Mvc;
using backend.Model;

namespace SensorController;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Sensor>> ObtenerDatos()
    {
        var sensors=new List<Sensor>
        {
            new Sensor {Id=1, Zona="Amazonas", Estado="Activo", Temperatura=32.5},
            new Sensor {Id=2, Zona="Ártico", Estado="Inactivo", Temperatura=-10.2},
            new Sensor {Id=3, Zona="Amazonas", Estado="Activo", Temperatura=31.8},
            new Sensor {Id=4, Zona="Sahara", Estado="Activo", Temperatura=45.1}
        };
        return Ok(sensors);
    }
}