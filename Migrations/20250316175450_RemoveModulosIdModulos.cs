using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiegoWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveModulosIdModulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoriaDeModulos_Modulos_ModulosId_Modulos",
                table: "HistoriaDeModulos");

            migrationBuilder.DropIndex(
                name: "IX_HistoriaDeModulos_ModulosId_Modulos",
                table: "HistoriaDeModulos");

            migrationBuilder.DropColumn(
                name: "ModulosId_Modulos",
                table: "HistoriaDeModulos");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaDeModulos_Id_Modulos",
                table: "HistoriaDeModulos",
                column: "Id_Modulos");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoriaDeModulos_Modulos_Id_Modulos",
                table: "HistoriaDeModulos",
                column: "Id_Modulos",
                principalTable: "Modulos",
                principalColumn: "Id_Modulos",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoriaDeModulos_Modulos_Id_Modulos",
                table: "HistoriaDeModulos");

            migrationBuilder.DropIndex(
                name: "IX_HistoriaDeModulos_Id_Modulos",
                table: "HistoriaDeModulos");

            migrationBuilder.AddColumn<int>(
                name: "ModulosId_Modulos",
                table: "HistoriaDeModulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaDeModulos_ModulosId_Modulos",
                table: "HistoriaDeModulos",
                column: "ModulosId_Modulos");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoriaDeModulos_Modulos_ModulosId_Modulos",
                table: "HistoriaDeModulos",
                column: "ModulosId_Modulos",
                principalTable: "Modulos",
                principalColumn: "Id_Modulos",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
