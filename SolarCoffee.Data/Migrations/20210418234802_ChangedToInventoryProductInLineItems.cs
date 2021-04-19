using Microsoft.EntityFrameworkCore.Migrations;

namespace SolarCoffee.Data.Migrations
{
    public partial class ChangedToInventoryProductInLineItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderItems_Products_ProductId",
                table: "SalesOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderItems_ProductId",
                table: "SalesOrderItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SalesOrderItems");

            migrationBuilder.AddColumn<int>(
                name: "InventoryProductId",
                table: "SalesOrderItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItems_InventoryProductId",
                table: "SalesOrderItems",
                column: "InventoryProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderItems_Products_InventoryProductId",
                table: "SalesOrderItems",
                column: "InventoryProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderItems_Products_InventoryProductId",
                table: "SalesOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderItems_InventoryProductId",
                table: "SalesOrderItems");

            migrationBuilder.DropColumn(
                name: "InventoryProductId",
                table: "SalesOrderItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SalesOrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItems_ProductId",
                table: "SalesOrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderItems_Products_ProductId",
                table: "SalesOrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
