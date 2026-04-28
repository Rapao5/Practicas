using Microsoft.EntityFrameworkCore;
using backend.Interfaces;
using backend.asignacionesRepository;
using backend.ecosistemaRepository;
using backend.investigadorRepository;
using backend.proyectoRepository;
using backend.appDbContext;
using backend.Services;
using System.Text.Json.Serialization; // 👈 Importante para los Enums

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------------------
// 1. Configuración de Base de Datos Condicional 🧠
// -------------------------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Si el entorno es "Testing", usamos InMemory para evitar conflictos con MySQL
    if (builder.Environment.IsEnvironment("Testing"))
    {
        options.UseInMemoryDatabase("TestDatabase");
    }
    else
    {
        // Esta es tu configuración original de MySQL 🐬
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var serverVersion = new MySqlServerVersion(new Version(10, 4, 0)); 
        
        options.UseMySql(connectionString, serverVersion);
    }
});

// -------------------------------------------------------------------------
// 2. Registro de Repositorios (Inyección de Dependencias) 💉
// -------------------------------------------------------------------------
builder.Services.AddScoped<IAsignacionesRepository, AsignacionesRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IEcosistemaRepository, EcosistemaRepository>();
builder.Services.AddScoped<IInvestigadorRepository, InvestigadorRepository>();

// -------------------------------------------------------------------------
// 3. Registro de Servicios (Lógica de Negocio) 🧠
// -------------------------------------------------------------------------
builder.Services.AddScoped<IAsignacionesService, AsignacionesService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IEcosistemaService, EcosistemaService>();
builder.Services.AddScoped<IInvestigadorService, InvestigadorService>();

builder.Services.AddAuthorization();

// -------------------------------------------------------------------------
// 4. Configuración de CORS 🌐
// -------------------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -------------------------------------------------------------------------
// 5. Configuración de Controladores y JSON ⚙️
// -------------------------------------------------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto soluciona el error "The JSON value could not be converted to backend.eco.Conservacion"
        // Permite que los Enums se envíen y reciban como texto en lugar de números.
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// -------------------------------------------------------------------------
// 6. Configuración del Pipeline de HTTP 🌊
// -------------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    // Aquí puedes poner app.UseSwagger() si lo necesitas
}

app.UseCors("AllowVueApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Requisito para WebApplicationFactory en proyectos de Test
public partial class Program { }