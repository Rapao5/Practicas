using backend.asignaciones;
using backend.eco;
using backend.investigador;
using backend.proyecto;
using Microsoft.EntityFrameworkCore;

namespace backend.appDbContext;

public class AppDbContext : DbContext
{

public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
{
}
public AppDbContext() { }

  public DbSet<Ecosistema> Ecosistemas {get; set;}
  public DbSet<Proyecto> Proyectos {get; set;}
  public DbSet<Investigador> Investigadores {get; set;}
  public DbSet<Asignaciones> Asignaciones {get; set;}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ecosistema>().ToTable("ecosistemas");
        modelBuilder.Entity<Ecosistema>()
                            .Property(e => e.Descripcion)
                            .HasColumnName("descripcionEcosistema")
                            .HasColumnType("varchar(500)");
        modelBuilder.Entity<Ecosistema>()
                            .Property(e => e.AreaLatitud)
                            .HasColumnName("areaLatitud")
                            .HasColumnType("float");
        modelBuilder.Entity<Ecosistema>()
                            .Property(e => e.AreaLongitud)
                            .HasColumnName("areaLongitud")
                            .HasColumnType("float");
        modelBuilder.Entity<Ecosistema>()
                            .Property(p => p.Conservacion)
                            .HasColumnName("conservacion")
                            .HasColumnType("bit");

        modelBuilder.Entity<Proyecto>().ToTable("proyectos");
        modelBuilder.Entity<Proyecto>()
                            .Property(p => p.Nombre)
                            .HasColumnName("nombreProyecto")
                            .HasColumnType("varchar(100)");
        modelBuilder.Entity<Proyecto>()
                            .Property(p => p.Presupuesto)
                            .HasColumnName("presupuesto")
                            .HasColumnType("float");
        modelBuilder.Entity<Proyecto>()
                            .Property(p => p.FechaInicio)
                            .HasColumnName("fechaInicio")
                            .HasColumnType("date");
        modelBuilder.Entity<Proyecto>()
                            .Property(p => p.FechaFinal)
                            .HasColumnName("fechaFinal")
                            .HasColumnType("date");
        modelBuilder.Entity<Proyecto>()
                            .Property(p => p.Estado)
                            .HasColumnName("estado")
                            .HasColumnType("bit");
        modelBuilder.Entity<Proyecto>()
                            .Property(p => p.EspecieFoco)
                            .HasColumnName("especieFoco")
                            .HasColumnType("varchar(100)");

        modelBuilder.Entity<Investigador>().ToTable("investigadores");
        modelBuilder.Entity<Investigador>()
                            .Property(p => p.Nombre)
                            .HasColumnName("nombreInvestigador")
                            .HasColumnType("varchar(200)");
        modelBuilder.Entity<Investigador>()
                            .Property(p => p.Email)
                            .HasColumnName("email")
                            .HasColumnType("varchar(500)");
        modelBuilder.Entity<Investigador>()
                            .Property(p => p.Experiencia)
                            .HasColumnName("experiencia")
                            .HasColumnType("int");

        modelBuilder.Entity<Asignaciones>().ToTable("asiganciones");
        modelBuilder.Entity<Asignaciones>()
                            .Property(p => p.Rol)
                            .HasColumnName("rol")
                            .HasColumnType("varchar(50)");
        modelBuilder.Entity<Asignaciones>()
                            .Property(p => p.ProyectoId)
                            .HasColumnName("proyectoId")
                            .HasColumnType("int");
        modelBuilder.Entity<Asignaciones>()
                            .Property(p => p.InvestigadorId)
                            .HasColumnName("investigadorId")
                            .HasColumnType("int");
    }
}