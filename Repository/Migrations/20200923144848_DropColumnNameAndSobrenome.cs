using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class DropColumnNameAndSobrenome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Amizades");

            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "Amizades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Amizades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "Amizades",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
