using Microsoft.EntityFrameworkCore.Migrations;

namespace Introductie.Data.Migrations
{
    public partial class Introductie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    KlantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    TrouwDatum = table.Column<string>(nullable: true),
                    Telnr = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.KlantID);
                });

            migrationBuilder.CreateTable(
                name: "Afspraken",
                columns: table => new
                {
                    AfspraakID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<string>(nullable: true),
                    Tijd = table.Column<string>(nullable: true),
                    KlantID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afspraken", x => x.AfspraakID);
                    table.ForeignKey(
                        name: "FK_Afspraken_Klanten_KlantID",
                        column: x => x.KlantID,
                        principalTable: "Klanten",
                        principalColumn: "KlantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afspraken_KlantID",
                table: "Afspraken",
                column: "KlantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Afspraken");

            migrationBuilder.DropTable(
                name: "Klanten");
        }
    }
}
