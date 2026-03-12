using Microsoft.AspNetCore.Mvc;
using backend.Model;

namespace SensorController;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    private static List<Sensor> sensors=new List<Sensor>
        {
            new Sensor {Id=1, Zona="Amazonas", Estado="Activo", Temperatura=32.5},
            new Sensor {Id=2, Zona="Ártico", Estado="Inactivo", Temperatura=-10.2},
            new Sensor {Id=3, Zona="Amazonas", Estado="Mantenimiento", Temperatura=31.8},
            new Sensor {Id=4, Zona="Sahara", Estado="Activo", Temperatura=45.1}
        };
        
    [HttpGet("{id}")]
    public ActionResult<Sensor> ObtenerSensorPorId(int id)
    {
        var sensor = sensors.FirstOrDefault(s => s.Id == id);
        if (sensor == null)
        {
            return NotFound();
        }
        return Ok(sensor);
    }
    [HttpGet]
    public ActionResult<IEnumerable<Sensor>> ObtenerDatos(
        [FromQuery] string? zona,
        [FromQuery] string? estado,
        [FromQuery] double? tempMin,
        [FromQuery] double? tempMax)
    {
        var consulta = sensors.AsQueryable();

        if (!string.IsNullOrEmpty(zona))
        {
            consulta = consulta.Where(s => s.Zona.Contains(zona, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(estado))
        {
            consulta = consulta.Where(s => s.Estado == estado);
        }

        if (tempMin.HasValue)
        {
            consulta = consulta.Where(s => s.Temperatura >= tempMin.Value);
        }

        if (tempMax.HasValue)
        {
            consulta = consulta.Where(s => s.Temperatura <= tempMax.Value);
        }

        return Ok(consulta.ToList());
    }
    [HttpPost]
    public ActionResult<Sensor> CrearSensor([FromBody] Sensor nuevoSensor)
    {
        if(nuevoSensor == null)
        {
            return BadRequest("Datos no válidos");
        }
        
        int nuevoId = sensors.Any() ? sensors.Max(s=>s.Id) + 1 : 1;
        nuevoSensor.Id = nuevoId;
        sensors.Add(nuevoSensor);

        return CreatedAtAction(nameof(ObtenerDatos), new {id=nuevoSensor.Id}, nuevoSensor);
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteSensor(int id)
    {
        var sensorExite = sensors.FirstOrDefault(s=>s.Id == id);
        if (sensorExite == null)
        {
            return NotFound($"No existe el sensor con ID {id}");
        }
        sensors.Remove(sensorExite);
        return NoContent();
    }
    [HttpPut]
    public ActionResult<Sensor> UpdateSensor([FromBody] Sensor sensor)
    {
        var sensorExite = sensors.FirstOrDefault(s=>s.Id==sensor.Id);
        if (sensorExite==null)
        {
            return NotFound($"No existe el sensor con ID {sensor.Id}");
        }
        sensorExite.Zona=sensor.Zona;
        sensorExite.Estado=sensor.Estado;
        sensorExite.Temperatura=sensor.Temperatura;
        
        return Ok(sensorExite);
    }
}