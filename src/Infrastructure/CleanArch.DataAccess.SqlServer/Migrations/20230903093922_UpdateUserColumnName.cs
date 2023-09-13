using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.DataAccess.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatorId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Products",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CreatorId",
                table: "Products",
                newName: "IX_Products_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_OwnerId",
                table: "Products",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_OwnerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Products",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OwnerId",
                table: "Products",
                newName: "IX_Products_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatorId",
                table: "Products",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
