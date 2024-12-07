using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CantidadHabitantes = table.Column<int>(type: "int", nullable: false),
                    Delegado_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delegado_Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCreador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_IdCreador",
                        column: x => x.IdCreador,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdDisciplina = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Disciplinas_IdDisciplina",
                        column: x => x.IdDisciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atletas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPais = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atletas_Paises_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtletaDisciplina",
                columns: table => new
                {
                    IdAtleta = table.Column<int>(type: "int", nullable: false),
                    IdDisciplina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtletaDisciplina", x => new { x.IdAtleta, x.IdDisciplina });
                    table.ForeignKey(
                        name: "FK_AtletaDisciplina_Atletas_IdAtleta",
                        column: x => x.IdAtleta,
                        principalTable: "Atletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtletaDisciplina_Disciplinas_IdDisciplina",
                        column: x => x.IdDisciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtletaEvento",
                columns: table => new
                {
                    IdAtleta = table.Column<int>(type: "int", nullable: false),
                    IdEvento = table.Column<int>(type: "int", nullable: false),
                    Puntaje = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtletaEvento", x => new { x.IdAtleta, x.IdEvento });
                    table.ForeignKey(
                        name: "FK_AtletaEvento_Atletas_IdAtleta",
                        column: x => x.IdAtleta,
                        principalTable: "Atletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtletaEvento_Eventos_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtletaDisciplina_IdDisciplina",
                table: "AtletaDisciplina",
                column: "IdDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_AtletaEvento_IdEvento",
                table: "AtletaEvento",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Atletas_IdPais",
                table: "Atletas",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_Nombre",
                table: "Disciplinas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_IdDisciplina",
                table: "Eventos",
                column: "IdDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Nombre",
                table: "Eventos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Nombre",
                table: "Paises",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdCreador",
                table: "Usuarios",
                column: "IdCreador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtletaDisciplina");

            migrationBuilder.DropTable(
                name: "AtletaEvento");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Atletas");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Disciplinas");
        }
    }
}
