using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LivroId",
                table: "Generos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    LivroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoPublicacao = table.Column<int>(type: "int", nullable: false),
                    ISBN10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlCapa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Editora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivroGeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.LivroId);
                });

            migrationBuilder.CreateTable(
                name: "LivrosGeneros",
                columns: table => new
                {
                    LivroGeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LivroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrosGeneros", x => x.LivroGeneroId);
                    table.ForeignKey(
                        name: "FK_LivrosGeneros_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivrosGeneros_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "LivroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Generos_LivroId",
                table: "Generos",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_LivroGeneroId",
                table: "Livros",
                column: "LivroGeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosGeneros_GeneroId",
                table: "LivrosGeneros",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosGeneros_LivroId",
                table: "LivrosGeneros",
                column: "LivroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Generos_Livros_LivroId",
                table: "Generos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "LivroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_LivrosGeneros_LivroGeneroId",
                table: "Livros",
                column: "LivroGeneroId",
                principalTable: "LivrosGeneros",
                principalColumn: "LivroGeneroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generos_Livros_LivroId",
                table: "Generos");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_LivrosGeneros_LivroGeneroId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "LivrosGeneros");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Generos_LivroId",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "Generos");
        }
    }
}
