
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB5.Migrations
{
    public partial class AddProductCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriiProduse",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "INTEGER",
                        nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),

                    Denumire = table.Column<string>(
                        type: "TEXT",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_CategoriiProduse",
                        x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CategoriiProduse",
                columns: new[] { "Id", "Denumire" },
                values: new object[] { 1, "Fara categorie" });

            migrationBuilder.AddColumn<int>(
                name: "IdCategorie",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.Sql(
                "UPDATE Products SET IdCategorie = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdCategorie",
                table: "Products",
                column: "IdCategorie");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriiProduse_IdCategorie",
                table: "Products",
                column: "IdCategorie",
                principalTable: "CategoriiProduse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriiProduse_IdCategorie",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IdCategorie",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IdCategorie",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CategoriiProduse");
        }
    }
}
