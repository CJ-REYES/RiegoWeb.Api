using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiegoWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class Historial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoriaDeModulos",
                columns: table => new
                {
                    Id_HistoriaDeModulos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Modulos = table.Column<int>(type: "int", nullable: false),
                    ModulosId_Modulos = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Temperatura = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Humedad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LuzNivel = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriaDeModulos", x => x.Id_HistoriaDeModulos);
                    table.ForeignKey(
                        name: "FK_HistoriaDeModulos_Modulos_ModulosId_Modulos",
                        column: x => x.ModulosId_Modulos,
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulos",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaDeModulos_ModulosId_Modulos",
                table: "HistoriaDeModulos",
                column: "ModulosId_Modulos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoriaDeModulos");
        }
    }
}
