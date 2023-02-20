using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SklepElektroniczny1501.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    IloscDostepna = table.Column<int>(nullable: false),
                    Cena = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zmowienia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumerZamowienia = table.Column<string>(nullable: true),
                    DataZamowienia = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zmowienia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProduktKatergorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduktu = table.Column<int>(nullable: false),
                    IdKategoria = table.Column<int>(nullable: false),
                    ProduktId = table.Column<int>(nullable: true),
                    KategoriaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktKatergorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProduktKatergorie_Kategorie_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduktKatergorie_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZamowienieProdukty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdukt = table.Column<int>(nullable: false),
                    IdZamowienie = table.Column<int>(nullable: false),
                    Ilosc = table.Column<int>(nullable: false),
                    Cena = table.Column<decimal>(nullable: false),
                    ZamowienieId = table.Column<int>(nullable: true),
                    ProduktId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZamowienieProdukty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZamowienieProdukty_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZamowienieProdukty_Zmowienia_ZamowienieId",
                        column: x => x.ZamowienieId,
                        principalTable: "Zmowienia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktKatergorie_KategoriaId",
                table: "ProduktKatergorie",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduktKatergorie_ProduktId",
                table: "ProduktKatergorie",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_ZamowienieProdukty_ProduktId",
                table: "ZamowienieProdukty",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_ZamowienieProdukty_ZamowienieId",
                table: "ZamowienieProdukty",
                column: "ZamowienieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktKatergorie");

            migrationBuilder.DropTable(
                name: "ZamowienieProdukty");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Zmowienia");
        }
    }
}
