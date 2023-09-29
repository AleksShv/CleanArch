using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.DataAccess.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "EntityHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "EntityHistories");
        }
    }
}
