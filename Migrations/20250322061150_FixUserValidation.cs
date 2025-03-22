using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiegoWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixUserValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Id_User",
                table: "Users",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Contraseña");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "Id_User");
        }
    }
}
