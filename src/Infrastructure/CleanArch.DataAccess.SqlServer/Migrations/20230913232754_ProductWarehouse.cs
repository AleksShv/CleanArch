using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.DataAccess.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ProductWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Products_ProductsId",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehousesId",
                table: "ProductWarehouse");

            migrationBuilder.RenameColumn(
                name: "WarehousesId",
                table: "ProductWarehouse",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductWarehouse",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouse_WarehousesId",
                table: "ProductWarehouse",
                newName: "IX_ProductWarehouse_WarehouseId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductWarehouse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Products_ProductId",
                table: "ProductWarehouse",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehouseId",
                table: "ProductWarehouse",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Products_ProductId",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehouseId",
                table: "ProductWarehouse");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductWarehouse");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "ProductWarehouse",
                newName: "WarehousesId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductWarehouse",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouse_WarehouseId",
                table: "ProductWarehouse",
                newName: "IX_ProductWarehouse_WarehousesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Products_ProductsId",
                table: "ProductWarehouse",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehousesId",
                table: "ProductWarehouse",
                column: "WarehousesId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
