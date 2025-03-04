using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiegoWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class cambiomodeluser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Contraseña");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Users",
                newName: "Email");
        }
    }
}
