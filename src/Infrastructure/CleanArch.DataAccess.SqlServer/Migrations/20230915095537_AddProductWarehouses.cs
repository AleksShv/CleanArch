using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.DataAccess.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddProductWarehouses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Products_ProductId",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehouseId",
                table: "ProductWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse");

            migrationBuilder.RenameTable(
                name: "ProductWarehouse",
                newName: "ProductWarehouses");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouse_WarehouseId",
                table: "ProductWarehouses",
                newName: "IX_ProductWarehouses_WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouses",
                table: "ProductWarehouses",
                columns: new[] { "ProductId", "WarehouseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouses_Products_ProductId",
                table: "ProductWarehouses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouses_Warehouses_WarehouseId",
                table: "ProductWarehouses",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouses_Products_ProductId",
                table: "ProductWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouses_Warehouses_WarehouseId",
                table: "ProductWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouses",
                table: "ProductWarehouses");

            migrationBuilder.RenameTable(
                name: "ProductWarehouses",
                newName: "ProductWarehouse");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouses_WarehouseId",
                table: "ProductWarehouse",
                newName: "IX_ProductWarehouse_WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse",
                columns: new[] { "ProductId", "WarehouseId" });

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
    }
}
