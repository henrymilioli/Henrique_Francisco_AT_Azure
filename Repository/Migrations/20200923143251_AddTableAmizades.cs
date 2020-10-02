using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddTableAmizades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amigos_Amigos_AmigoId",
                table: "Amigos");

            migrationBuilder.DropIndex(
                name: "IX_Amigos_AmigoId",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "AmigoId",
                table: "Amigos");

            migrationBuilder.CreateTable(
                name: "Amizades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    AmigoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amizades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amizades_Amigos_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "Amigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amizades_AmigoId",
                table: "Amizades",
                column: "AmigoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amizades");

            migrationBuilder.AddColumn<Guid>(
                name: "AmigoId",
                table: "Amigos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_AmigoId",
                table: "Amigos",
                column: "AmigoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amigos_Amigos_AmigoId",
                table: "Amigos",
                column: "AmigoId",
                principalTable: "Amigos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
