using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using backend.appDbContext;
using backend.eco;
using Biodiversidad.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Biodiversidad.Tests.IntegrationTests;

public class EcosistemaIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient client;
    private readonly CustomWebApplicationFactory factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public EcosistemaIntegrationTests(CustomWebApplicationFactory factory)
    {
        this.factory = factory;
        this.client = factory.CreateClient();

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        _jsonOptions.Converters.Add(new JsonStringEnumConverter());
    }

    [Fact]
    public async Task GetEcositemas()
    {
        using (var scope = factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            context.Ecosistemas.RemoveRange(context.Ecosistemas);
            await context.SaveChangesAsync();

            var eco1 = new Ecosistema 
            {
                Nombre = "Bosque Tropical",
                Descripcion = "Verde", 
                Area = 20, 
                AreaLatitud = 20, 
                AreaLongitud = 20, 
                Conservacion = Conservacion.EnPeligro
            };

            var eco2 = new Ecosistema
            {
                Nombre = "Desierto",
                Descripcion = "Seco",
                Area = 20,
                AreaLatitud = 20,
                AreaLongitud = 20,
                Conservacion = Conservacion.Vulnerable
            };

            context.Ecosistemas.AddRange(eco1, eco2);
            await context.SaveChangesAsync();
        }
        
        var response = await client.GetAsync("/api/ecosistema");
        
        response.EnsureSuccessStatusCode(); 

        var ecosistemas = await response.Content.ReadFromJsonAsync<List<Ecosistema>>(_jsonOptions);

        Assert.NotNull(ecosistemas);
        Assert.Equal(2, ecosistemas.Count);
        Assert.Contains(ecosistemas, e => e.Nombre == "Bosque Tropical");
        Assert.Contains(ecosistemas, e => e.Nombre == "Desierto");
    }
    
  [Fact]
  public async Task PostProyecto_PresupuestoNegativo()
  {
    var proyectoInvalido = new 
    {
        Nombre = "Proyecto de Reforestación",
        Presupuesto = -500,
        FechaInicio = DateOnly.FromDateTime(DateTime.Now),
        FechaFinal = DateOnly.FromDateTime(DateTime.Now),
        Estado = true,
        EspecieFoco = "Lagarto",
        EcosistemaId = 1
    };

    var response = await client.PostAsJsonAsync("/api/proyecto", proyectoInvalido);

    Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Fact]
  public async Task PostProyecto_Valido()
  {
     using (var scope = factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            context.Ecosistemas.RemoveRange(context.Ecosistemas);
            await context.SaveChangesAsync();

            var eco1 = new Ecosistema 
            {
                Nombre = "Bosque Tropical",
                Descripcion = "Verde", 
                Area = 20, 
                AreaLatitud = 20, 
                AreaLongitud = 20, 
                Conservacion = Conservacion.EnPeligro
            };

            context.Ecosistemas.AddRange(eco1);
            await context.SaveChangesAsync();
        }
    var proyectoValido = new 
    {
        Nombre = "Proyecto de Reforestación",
        Presupuesto = 500,
        FechaInicio = DateOnly.FromDateTime(DateTime.Now),
        FechaFinal = DateOnly.FromDateTime(DateTime.Now),
        Estado = true,
        EspecieFoco = "Lagarto",
        EcosistemaId = 1
    };

    var response = await client.PostAsJsonAsync("/api/proyecto", proyectoValido);

    Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
  }
}