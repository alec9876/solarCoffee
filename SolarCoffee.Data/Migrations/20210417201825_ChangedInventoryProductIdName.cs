using Microsoft.EntityFrameworkCore.Migrations;

namespace SolarCoffee.Data.Migrations
{
    public partial class ChangedInventoryProductIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventories_Products_ProductId",
                table: "ProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventories_ProductId",
                table: "ProductInventories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductInventories");

            migrationBuilder.AddColumn<int>(
                name: "InventoryProductId",
                table: "ProductInventories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_InventoryProductId",
                table: "ProductInventories",
                column: "InventoryProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventories_Products_InventoryProductId",
                table: "ProductInventories",
                column: "InventoryProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventories_Products_InventoryProductId",
                table: "ProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventories_InventoryProductId",
                table: "ProductInventories");

            migrationBuilder.DropColumn(
                name: "InventoryProductId",
                table: "ProductInventories");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductInventories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_ProductId",
                table: "ProductInventories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventories_Products_ProductId",
                table: "ProductInventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
