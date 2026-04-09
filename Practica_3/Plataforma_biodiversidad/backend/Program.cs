using Microsoft.EntityFrameworkCore;
using backend.Interfaces;
using backend.asignacionesRepository;
using backend.ecosistemaRepository;
using backend.investigadorRepository;
using backend.proyectoRepository;
using backend.appDbContext;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// ... (tus otros servicios)

// 2. Configuración para MySQL 🐬
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(10, 4, 0)); // La versión que tienes en tu DbContext
    
    options.UseMySql(connectionString, serverVersion);
});

// 3. Registro de tus Repositorios (Inyección de Dependencias) 💉
builder.Services.AddScoped<IAsignacionesRepository, AsignacionesRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IEcosistemaRepository, EcosistemaRepository>();
builder.Services.AddScoped<IInvestigadorRepository, InvestigadorRepository>();
// 5. Registro de tus Servicios (Lógica de Negocio) 🧠
builder.Services.AddScoped<IAsignacionesService, AsignacionesService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IEcosistemaService, EcosistemaService>();
builder.Services.AddScoped<IInvestigadorService, InvestigadorService>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

// 4. Configuración del Pipeline de HTTP 🌊
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();