using Microsoft.EntityFrameworkCore;
using backend.Interfaces;
using backend.asignacionesRepository;
using backend.ecosistemaRepository;
using backend.investigadorRepository;
using backend.proyectoRepository;
using backend.appDbContext;
using backend.Services;
using System.Text.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsEnvironment("Testing"))
    {
        options.UseInMemoryDatabase("TestDatabase");
    }
    else
    {
      
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var serverVersion = new MySqlServerVersion(new Version(10, 4, 0)); 
        
        options.UseMySql(connectionString, serverVersion);
    }
});


builder.Services.AddScoped<IAsignacionesRepository, AsignacionesRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IEcosistemaRepository, EcosistemaRepository>();
builder.Services.AddScoped<IInvestigadorRepository, InvestigadorRepository>();


builder.Services.AddScoped<IAsignacionesService, AsignacionesService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IEcosistemaService, EcosistemaService>();
builder.Services.AddScoped<IInvestigadorService, InvestigadorService>();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        
        if (context.Database.IsRelational())
        {
            context.Database.Migrate(); 
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al migrar la base de datos.");
        throw; 
    }
}


if (app.Environment.IsDevelopment())
{

}

app.UseCors("AllowVueApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }