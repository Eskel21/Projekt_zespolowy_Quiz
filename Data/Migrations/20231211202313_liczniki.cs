using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_Quizy.Data.Migrations
{
    public partial class liczniki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "licznik",
                table: "Dzialy",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "licznik",
                table: "Dzialy");
        }
    }
}
