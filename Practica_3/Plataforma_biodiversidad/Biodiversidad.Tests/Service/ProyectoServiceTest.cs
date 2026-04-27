using Moq;
using backend.Interfaces;
using backend.Services;
using backend.proyectoDTO;
using backend.proyecto;
using backend.investigador;
using backend.asignaciones;
using backend.eco;
using backend.asignacionesDTO;

namespace Biodiversidad.Tests.Service;

public class ProyectoServiceTest
{
  [Fact]
  public async Task CrearAsync_EstadoDeConservacion_EnPeligro() //test 1
  {

    var mockEcosistemaRepo = new Mock<IEcosistemaRepository>();
    var mockProyectoRepo = new Mock<IProyectoRepository>();

    var ecosistema = new Ecosistema
    {
      Id = 1,
      Nombre = "Sabana",
      Descripcion = "Sol",
      Area = 20,
      AreaLatitud = 20,
      AreaLongitud = 20,
      Conservacion = Conservacion.EnPeligro
    };

    mockEcosistemaRepo.Setup(repo => repo.GetByIdBasicAsync(1)).ReturnsAsync(ecosistema);

    var proyecto = new ProyectoDTO
    {
      Id = 1,
      Nombre = "AAA",
      Presupuesto = 1000,
      FechaInicio = DateOnly.FromDateTime(DateTime.Now),
      FechaFinal = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
      Estado = false,
      EspecieFoco = "León",
      EcosistemaId = 1
    };

    var servicio = new ProyectoService(mockProyectoRepo.Object, mockEcosistemaRepo.Object);

    var resultado = await servicio.CrearAsync(proyecto);

    Assert.NotNull(resultado);
    Assert.Equal(proyecto.Nombre, resultado.Nombre);

    mockProyectoRepo.Verify(m => m.AddAsync(It.IsAny<Proyecto>()), Times.Once());
  }

  [Fact]
  public async Task CrearAsync_Presupuesto_Negativo() //test 2
  {
    var mockEcosistemaRepo = new Mock<IEcosistemaRepository>();
    var mockProyectoRepo = new Mock<IProyectoRepository>();

    var ecosistema = new Ecosistema
    {
      Id = 1,
      Nombre = "Sabana",
      Descripcion = "Sol",
      Area = 20,
      AreaLatitud = 20,
      AreaLongitud = 20,
      Conservacion = Conservacion.EnPeligro
    };

    mockEcosistemaRepo.Setup(repo => repo.GetByIdBasicAsync(1)).ReturnsAsync(ecosistema);

    var proyecto = new ProyectoDTO
    {
      Id = 1,
      Nombre = "AAA",
      Presupuesto = -100,
      FechaInicio = DateOnly.FromDateTime(DateTime.Now),
      FechaFinal = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
      Estado = false,
      EspecieFoco = "León",
      EcosistemaId = 1
    };

    var servicio = new ProyectoService(mockProyectoRepo.Object, mockEcosistemaRepo.Object);
    var excepcion = await Assert.ThrowsAsync<ArgumentException>(() => servicio.CrearAsync(proyecto));
    await Assert.ThrowsAsync<ArgumentException>(() => servicio.CrearAsync(proyecto));
    Assert.Equal("El presupuesto del proyecto es menor a 0.", excepcion.Message);

    mockProyectoRepo.Verify(m => m.AddAsync(It.IsAny<Proyecto>()), Times.Never());
  }

  [Fact]
  public async Task CrearAsync_EstadoDeConservacion_Intacto() //test 3
  {
    var mockEcosistemaRepo = new Mock<IEcosistemaRepository>();
    var mockProyectoRepo = new Mock<IProyectoRepository>();

    var ecosistema = new Ecosistema
    {
      Id = 1,
      Nombre = "Sabana",
      Descripcion = "Sol",
      Area = 20,
      AreaLatitud = 20,
      AreaLongitud = 20,
      Conservacion = Conservacion.Intacto
    };

    mockEcosistemaRepo.Setup(repo => repo.GetByIdBasicAsync(1)).ReturnsAsync(ecosistema);

    var proyecto = new ProyectoDTO
    {
      Id = 1,
      Nombre = "AAA",
      Presupuesto = 2100,
      FechaInicio = DateOnly.FromDateTime(DateTime.Now),
      FechaFinal = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
      Estado = false,
      EspecieFoco = "León",
      EcosistemaId = 1
    };

    var servicio = new ProyectoService(mockProyectoRepo.Object, mockEcosistemaRepo.Object);
    var excepcion = await Assert.ThrowsAsync<ArgumentException>(() => servicio.CrearAsync(proyecto));
    await Assert.ThrowsAsync<ArgumentException>(() => servicio.CrearAsync(proyecto));
    Assert.Equal("No se puede asignar un proyecto en un ecosistema intacto", excepcion.Message);

    mockProyectoRepo.Verify(m => m.AddAsync(It.IsAny<Proyecto>()), Times.Never());
  }

  [Fact]
  public async Task CrearAsync_InvestigadorLiderCampo_DosProyectos() //test 4
  {
    var mockAsignacionRepo = new Mock<IAsignacionesRepository>();
    var mockInvestigadorRepo = new Mock<IInvestigadorRepository>();
    var mockProyectoRepo = new Mock<IProyectoRepository>();

    var investigador = new Investigador
    {
      Id = 1,
      Nombre = "Pablo",
      Email = "pablo@mail.com",
      Experiencia = Experiencia.Junior
    };

    var proyectoExistente = new Proyecto
    {
      Id = 1,
      Nombre = "AAA",
      Presupuesto = 2100,
      FechaInicio = DateOnly.FromDateTime(DateTime.Now),
      FechaFinal = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
      Estado = true,
      EspecieFoco = "León",
      EcosistemaId = 1
    };

    var asignacion = new Asignaciones
    {
      Id = 1,
      Rol = Rol.LiderCampo,
      FechaEntrada = DateOnly.FromDateTime(DateTime.Now),
      ProyectoId = 1,
      InvestigadorId = 1
    };

    mockProyectoRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(proyectoExistente);

    mockInvestigadorRepo.Setup(inves => inves.GetByIdAsync(1)).ReturnsAsync(investigador);

    var listaAsignaciones = new List<Asignaciones> { asignacion };

    mockAsignacionRepo.Setup(repo => repo.GetAsignacionesAsync()).ReturnsAsync(listaAsignaciones);

    var servicio = new AsignacionesService(mockAsignacionRepo.Object, mockInvestigadorRepo.Object, mockProyectoRepo.Object);

    var nuevaAsignacionLider = new AsignacionesDTO
    {
        Id = 2,
        Rol = "LiderCampo",
        FechaEntrada = DateOnly.FromDateTime(DateTime.Now),
        ProyectoId = 2,
        InvestigadorId = 1
    };

    var excepcion = await Assert.ThrowsAsync<ArgumentException>(() => 
        servicio.CrearAsync(nuevaAsignacionLider)
    );

    Assert.Equal("El investigador ya es lider de campo en otro proyecto", excepcion.Message);

    mockAsignacionRepo.Verify(m => m.AddAsync(It.IsAny<Asignaciones>()), Times.Never());
  }
}