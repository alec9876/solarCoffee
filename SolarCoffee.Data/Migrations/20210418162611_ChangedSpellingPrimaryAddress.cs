using Microsoft.EntityFrameworkCore.Migrations;

namespace SolarCoffee.Data.Migrations
{
    public partial class ChangedSpellingPrimaryAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAdressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PrimaryAdressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PrimaryAdressId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryAddressId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PrimaryAddressId",
                table: "Customers",
                column: "PrimaryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAddressId",
                table: "Customers",
                column: "PrimaryAddressId",
                principalTable: "CustomerAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PrimaryAddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PrimaryAddressId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryAdressId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PrimaryAdressId",
                table: "Customers",
                column: "PrimaryAdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAdressId",
                table: "Customers",
                column: "PrimaryAdressId",
                principalTable: "CustomerAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
