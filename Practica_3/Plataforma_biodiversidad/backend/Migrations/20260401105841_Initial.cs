using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_investigadores_proyectos_ProyectoId",
                table: "investigadores");

            migrationBuilder.DropIndex(
                name: "IX_investigadores_ProyectoId",
                table: "investigadores");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "investigadores");

            migrationBuilder.AddColumn<int>(
                name: "EcosistemaId",
                table: "proyectos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaEntrada",
                table: "asiganciones",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateIndex(
                name: "IX_proyectos_EcosistemaId",
                table: "proyectos",
                column: "EcosistemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_proyectos_ecosistemas_EcosistemaId",
                table: "proyectos",
                column: "EcosistemaId",
                principalTable: "ecosistemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_proyectos_ecosistemas_EcosistemaId",
                table: "proyectos");

            migrationBuilder.DropIndex(
                name: "IX_proyectos_EcosistemaId",
                table: "proyectos");

            migrationBuilder.DropColumn(
                name: "EcosistemaId",
                table: "proyectos");

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "investigadores",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEntrada",
                table: "asiganciones",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateIndex(
                name: "IX_investigadores_ProyectoId",
                table: "investigadores",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_investigadores_proyectos_ProyectoId",
                table: "investigadores",
                column: "ProyectoId",
                principalTable: "proyectos",
                principalColumn: "Id");
        }
    }
}
