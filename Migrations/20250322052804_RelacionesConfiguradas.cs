using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiegoWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesConfiguradas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Id_User",
                table: "Modulos",
                column: "Id_User");

            migrationBuilder.CreateIndex(
                name: "IX_LecturaModulo_Id_Modulo",
                table: "LecturaModulo",
                column: "Id_Modulo");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturaModulo_Modulos_Id_Modulo",
                table: "LecturaModulo",
                column: "Id_Modulo",
                principalTable: "Modulos",
                principalColumn: "Id_Modulo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_Users_Id_User",
                table: "Modulos",
                column: "Id_User",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturaModulo_Modulos_Id_Modulo",
                table: "LecturaModulo");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_Users_Id_User",
                table: "Modulos");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_Id_User",
                table: "Modulos");

            migrationBuilder.DropIndex(
                name: "IX_LecturaModulo_Id_Modulo",
                table: "LecturaModulo");
        }
    }
}
