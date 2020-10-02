using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    UrlFoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UrlFoto = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PaisId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Amigos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UrlFoto = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DtAniversario = table.Column<DateTime>(nullable: false),
                    PaisId = table.Column<Guid>(nullable: true),
                    EstadoId = table.Column<Guid>(nullable: true),
                    AmigoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amigos_Amigos_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "Amigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Amigos_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Amigos_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_AmigoId",
                table: "Amigos",
                column: "AmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_EstadoId",
                table: "Amigos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_PaisId",
                table: "Amigos",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_PaisId",
                table: "Estado",
                column: "PaisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amigos");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
