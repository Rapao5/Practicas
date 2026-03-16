using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Model;
using backend.ModelDTO;
using backend.Data;

namespace SensorController;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    /*private static List<Sensor> sensors=new List<Sensor>
        {
            new Sensor {Id=1, Zona="Amazonas", Estado="Activo", Temperatura=32.5},
            new Sensor {Id=2, Zona="Ártico", Estado="Inactivo", Temperatura=-10.2},
            new Sensor {Id=3, Zona="Amazonas", Estado="Mantenimiento", Temperatura=31.8},
            new Sensor {Id=4, Zona="Sahara", Estado="Activo", Temperatura=45.1}
        };*/

    private readonly AppDbContext context;

    public SensorsController(AppDbContext context)
    {
        this.context=context;
    }
        
    [HttpGet("{id}")]
    public async Task<ActionResult<Sensor>> ObtenerSensorPorId(int id)
    {
        var sensor = await context.Sensors.FindAsync(id);
        if (sensor == null)
        {
            return NotFound();
        }
        return Ok(sensor);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sensor>>> ObtenerDatos(
        [FromQuery] string? zona,
        [FromQuery] string? estado,
        [FromQuery] double? tempMin,
        [FromQuery] double? tempMax)
    {
        var consulta = context.Sensors.AsQueryable();

        if (!string.IsNullOrEmpty(zona))
        {
            consulta = consulta.Where(s => s.Zona.Contains(zona));
        }

        if (!string.IsNullOrEmpty(estado))
        {
            consulta = consulta.Where(s => s.Estado == estado);
        }

        if (tempMin.HasValue)
        {
            consulta = consulta.Where(s => s.Temperatura > tempMin.Value);
        }

        if (tempMax.HasValue)
        {
            consulta = consulta.Where(s => s.Temperatura < tempMax.Value);
        }

        var resultados = await consulta
            .Select(s => new SensorDTO 
            {
                Id = s.Id,
                Zona = s.Zona,
                Estado = s.Estado,
                Temperatura = s.Temperatura
            })
            .ToListAsync();
        
        return Ok(resultados);
    }
    [HttpPost]
    public  async Task <ActionResult<Sensor>> CrearSensor([FromBody] Sensor nuevoSensor)
    {
        if(nuevoSensor == null)
        {
            return BadRequest("Datos no válidos");
        }
        
        context.Sensors.Add(nuevoSensor);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerSensorPorId), new { id = nuevoSensor.Id }, nuevoSensor);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSensor(int id)
    {
        var sensorExite = await context.Sensors.FindAsync(id);
        if (sensorExite == null)
        {
            return NotFound($"No existe el sensor con ID {id}");
        }
        context.Sensors.Remove(sensorExite);
        await context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut]
    public async Task<ActionResult<Sensor>> UpdateSensor([FromBody] Sensor sensor)
    {
        var sensorExite = await context.Sensors.FindAsync(sensor.Id);
        if (sensorExite==null)
        {
            return NotFound($"No existe el sensor con ID {sensor.Id}");
        }
        sensorExite.Zona=sensor.Zona;
        sensorExite.Estado=sensor.Estado;
        sensorExite.Temperatura=sensor.Temperatura;
        
        await context.SaveChangesAsync();
        return Ok(sensorExite);
    }
}