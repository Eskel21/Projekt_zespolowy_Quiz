using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_Quizy.Data.Migrations
{
    public partial class migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Dzialy",
                columns: table => new
                {
                    DzialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dzialy", x => x.DzialId);
                });

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    KategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DzialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.KategoriaId);
                    table.ForeignKey(
                        name: "FK_Kategorie_Dzialy_DzialId",
                        column: x => x.DzialId,
                        principalTable: "Dzialy",
                        principalColumn: "DzialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pytania",
                columns: table => new
                {
                    PytanieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tresc = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PoziomTrudnosci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer_A = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer_B = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer_C = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer_D = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer_E = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer_F = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer_A_Correct = table.Column<bool>(type: "bit", nullable: true),
                    Answer_B_Correct = table.Column<bool>(type: "bit", nullable: true),
                    Answer_C_Correct = table.Column<bool>(type: "bit", nullable: true),
                    Answer_D_Correct = table.Column<bool>(type: "bit", nullable: true),
                    Answer_E_Correct = table.Column<bool>(type: "bit", nullable: true),
                    Answer_F_Correct = table.Column<bool>(type: "bit", nullable: true),
                    KategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pytania", x => x.PytanieId);
                    table.ForeignKey(
                        name: "FK_Pytania_Kategorie_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "KategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategorie_DzialId",
                table: "Kategorie",
                column: "DzialId");

            migrationBuilder.CreateIndex(
                name: "IX_Pytania_KategoriaId",
                table: "Pytania",
                column: "KategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pytania");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Dzialy");

    
        }
    }
}
