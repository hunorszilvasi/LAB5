using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB5.Migrations
{
    /// <inheritdoc />
    public partial class PreserveSalesHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IstoricVanzari_Products_IdProdus",
                table: "IstoricVanzari");

            migrationBuilder.AddForeignKey(
                name: "FK_IstoricVanzari_Products_IdProdus",
                table: "IstoricVanzari",
                column: "IdProdus",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IstoricVanzari_Products_IdProdus",
                table: "IstoricVanzari");

            migrationBuilder.AddForeignKey(
                name: "FK_IstoricVanzari_Products_IdProdus",
                table: "IstoricVanzari",
                column: "IdProdus",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
