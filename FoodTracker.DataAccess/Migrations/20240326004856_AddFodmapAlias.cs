using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFodmapAlias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "FodmapAliases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Global",
                table: "FodmapAliases",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FodmapAliases");

            migrationBuilder.DropColumn(
                name: "Global",
                table: "FodmapAliases");
        }
    }
}
