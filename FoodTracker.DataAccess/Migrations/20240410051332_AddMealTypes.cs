using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddMealTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSafeFoods_AspNetUsers_AppUserId",
                table: "UserSafeFoods");

            migrationBuilder.DropIndex(
                name: "IX_UserSafeFoods_AppUserId",
                table: "UserSafeFoods");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserSafeFoods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "MealTypeId",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MealTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealTypeId",
                table: "Meals",
                column: "MealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealTypes_MealTypeId",
                table: "Meals",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealTypes_MealTypeId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "MealTypes");

            migrationBuilder.DropIndex(
                name: "IX_Meals_MealTypeId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Meals");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserSafeFoods",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserSafeFoods_AppUserId",
                table: "UserSafeFoods",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSafeFoods_AspNetUsers_AppUserId",
                table: "UserSafeFoods",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
