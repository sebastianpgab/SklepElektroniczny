using Microsoft.EntityFrameworkCore.Migrations;

namespace SklepElektroniczny1501.Migrations
{
    public partial class nameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProduktKatergorie_Kategorie_KategoriaId",
                table: "ProduktKatergorie");

            migrationBuilder.DropForeignKey(
                name: "FK_ProduktKatergorie_Produkty_ProduktId",
                table: "ProduktKatergorie");

            migrationBuilder.DropForeignKey(
                name: "FK_ZamowienieProdukty_Produkty_ProduktId",
                table: "ZamowienieProdukty");

            migrationBuilder.DropForeignKey(
                name: "FK_ZamowienieProdukty_Zmowienia_ZamowienieId",
                table: "ZamowienieProdukty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zmowienia",
                table: "Zmowienia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZamowienieProdukty",
                table: "ZamowienieProdukty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProduktKatergorie",
                table: "ProduktKatergorie");

            migrationBuilder.RenameTable(
                name: "Zmowienia",
                newName: "Zamowienia");

            migrationBuilder.RenameTable(
                name: "ZamowienieProdukty",
                newName: "ZamowienieProdukt");

            migrationBuilder.RenameTable(
                name: "ProduktKatergorie",
                newName: "ProduktKatergoria");

            migrationBuilder.RenameIndex(
                name: "IX_ZamowienieProdukty_ZamowienieId",
                table: "ZamowienieProdukt",
                newName: "IX_ZamowienieProdukt_ZamowienieId");

            migrationBuilder.RenameIndex(
                name: "IX_ZamowienieProdukty_ProduktId",
                table: "ZamowienieProdukt",
                newName: "IX_ZamowienieProdukt_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_ProduktKatergorie_ProduktId",
                table: "ProduktKatergoria",
                newName: "IX_ProduktKatergoria_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_ProduktKatergorie_KategoriaId",
                table: "ProduktKatergoria",
                newName: "IX_ProduktKatergoria_KategoriaId");

            migrationBuilder.AlterColumn<int>(
                name: "Ilosc",
                table: "ZamowienieProdukt",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cena",
                table: "ZamowienieProdukt",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zamowienia",
                table: "Zamowienia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZamowienieProdukt",
                table: "ZamowienieProdukt",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProduktKatergoria",
                table: "ProduktKatergoria",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduktKatergoria_Kategorie_KategoriaId",
                table: "ProduktKatergoria",
                column: "KategoriaId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProduktKatergoria_Produkty_ProduktId",
                table: "ProduktKatergoria",
                column: "ProduktId",
                principalTable: "Produkty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZamowienieProdukt_Produkty_ProduktId",
                table: "ZamowienieProdukt",
                column: "ProduktId",
                principalTable: "Produkty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZamowienieProdukt_Zamowienia_ZamowienieId",
                table: "ZamowienieProdukt",
                column: "ZamowienieId",
                principalTable: "Zamowienia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProduktKatergoria_Kategorie_KategoriaId",
                table: "ProduktKatergoria");

            migrationBuilder.DropForeignKey(
                name: "FK_ProduktKatergoria_Produkty_ProduktId",
                table: "ProduktKatergoria");

            migrationBuilder.DropForeignKey(
                name: "FK_ZamowienieProdukt_Produkty_ProduktId",
                table: "ZamowienieProdukt");

            migrationBuilder.DropForeignKey(
                name: "FK_ZamowienieProdukt_Zamowienia_ZamowienieId",
                table: "ZamowienieProdukt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZamowienieProdukt",
                table: "ZamowienieProdukt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zamowienia",
                table: "Zamowienia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProduktKatergoria",
                table: "ProduktKatergoria");

            migrationBuilder.RenameTable(
                name: "ZamowienieProdukt",
                newName: "ZamowienieProdukty");

            migrationBuilder.RenameTable(
                name: "Zamowienia",
                newName: "Zmowienia");

            migrationBuilder.RenameTable(
                name: "ProduktKatergoria",
                newName: "ProduktKatergorie");

            migrationBuilder.RenameIndex(
                name: "IX_ZamowienieProdukt_ZamowienieId",
                table: "ZamowienieProdukty",
                newName: "IX_ZamowienieProdukty_ZamowienieId");

            migrationBuilder.RenameIndex(
                name: "IX_ZamowienieProdukt_ProduktId",
                table: "ZamowienieProdukty",
                newName: "IX_ZamowienieProdukty_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_ProduktKatergoria_ProduktId",
                table: "ProduktKatergorie",
                newName: "IX_ProduktKatergorie_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_ProduktKatergoria_KategoriaId",
                table: "ProduktKatergorie",
                newName: "IX_ProduktKatergorie_KategoriaId");

            migrationBuilder.AlterColumn<int>(
                name: "Ilosc",
                table: "ZamowienieProdukty",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Cena",
                table: "ZamowienieProdukty",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZamowienieProdukty",
                table: "ZamowienieProdukty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zmowienia",
                table: "Zmowienia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProduktKatergorie",
                table: "ProduktKatergorie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduktKatergorie_Kategorie_KategoriaId",
                table: "ProduktKatergorie",
                column: "KategoriaId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProduktKatergorie_Produkty_ProduktId",
                table: "ProduktKatergorie",
                column: "ProduktId",
                principalTable: "Produkty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZamowienieProdukty_Produkty_ProduktId",
                table: "ZamowienieProdukty",
                column: "ProduktId",
                principalTable: "Produkty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZamowienieProdukty_Zmowienia_ZamowienieId",
                table: "ZamowienieProdukty",
                column: "ZamowienieId",
                principalTable: "Zmowienia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
