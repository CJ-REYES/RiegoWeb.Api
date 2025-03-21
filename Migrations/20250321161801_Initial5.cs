using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiegoWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoriaDeModulos");

            migrationBuilder.DropTable(
                name: "MyModulos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modulos",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "Humedad",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "LuzNivel",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "Temperatura",
                table: "Modulos");

            migrationBuilder.RenameColumn(
                name: "Id_Modulos",
                table: "Modulos",
                newName: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "date",
                table: "Modulos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Modulos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id_User",
                table: "Modulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Modulos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modulos",
                table: "Modulos",
                column: "id");

            migrationBuilder.CreateTable(
                name: "LecturaModulo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_Modulo = table.Column<int>(type: "int", nullable: false),
                    Temperatura = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Humedad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LuzNivel = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturaModulo", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LecturaModulo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modulos",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "Id_User",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Modulos");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Modulos",
                newName: "Id_Modulos");

            migrationBuilder.AlterColumn<int>(
                name: "Id_Modulos",
                table: "Modulos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Humedad",
                table: "Modulos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LuzNivel",
                table: "Modulos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Temperatura",
                table: "Modulos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modulos",
                table: "Modulos",
                column: "Id_Modulos");

            migrationBuilder.CreateTable(
                name: "HistoriaDeModulos",
                columns: table => new
                {
                    Id_HistoriaDeModulos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Modulos = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Humedad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LuzNivel = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Temperatura = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriaDeModulos", x => x.Id_HistoriaDeModulos);
                    table.ForeignKey(
                        name: "FK_HistoriaDeModulos_Modulos_Id_Modulos",
                        column: x => x.Id_Modulos,
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulos",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MyModulos",
                columns: table => new
                {
                    IdMyModulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Modulo = table.Column<int>(type: "int", nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyModulos", x => x.IdMyModulo);
                    table.ForeignKey(
                        name: "FK_MyModulos_Modulos_Id_Modulo",
                        column: x => x.Id_Modulo,
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyModulos_Users_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Users",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaDeModulos_Id_Modulos",
                table: "HistoriaDeModulos",
                column: "Id_Modulos");

            migrationBuilder.CreateIndex(
                name: "IX_MyModulos_Id_Modulo",
                table: "MyModulos",
                column: "Id_Modulo");

            migrationBuilder.CreateIndex(
                name: "IX_MyModulos_Id_User",
                table: "MyModulos",
                column: "Id_User");
        }
    }
}
