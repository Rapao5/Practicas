using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ecosistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcionEcosistema = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    areaLatitud = table.Column<float>(type: "float", nullable: false),
                    areaLongitud = table.Column<float>(type: "float", nullable: false),
                    conservacion = table.Column<ulong>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ecosistemas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombreProyecto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    presupuesto = table.Column<float>(type: "float", nullable: false),
                    fechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaFinal = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<ulong>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proyectos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "investigadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombreInvestigador = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    experiencia = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investigadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_investigadores_proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "proyectos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asiganciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rol = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaEntrada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    proyectoId = table.Column<int>(type: "int", nullable: false),
                    investigadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asiganciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asiganciones_investigadores_investigadorId",
                        column: x => x.investigadorId,
                        principalTable: "investigadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_asiganciones_proyectos_proyectoId",
                        column: x => x.proyectoId,
                        principalTable: "proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_asiganciones_investigadorId",
                table: "asiganciones",
                column: "investigadorId");

            migrationBuilder.CreateIndex(
                name: "IX_asiganciones_proyectoId",
                table: "asiganciones",
                column: "proyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_investigadores_ProyectoId",
                table: "investigadores",
                column: "ProyectoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asiganciones");

            migrationBuilder.DropTable(
                name: "ecosistemas");

            migrationBuilder.DropTable(
                name: "investigadores");

            migrationBuilder.DropTable(
                name: "proyectos");
        }
    }
}
