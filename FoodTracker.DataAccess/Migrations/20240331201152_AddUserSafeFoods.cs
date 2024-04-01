using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSafeFoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSafeFoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSafeFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSafeFoods_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSafeFoods_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSafeFoods_AppUserId",
                table: "UserSafeFoods",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSafeFoods_FoodId",
                table: "UserSafeFoods",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSafeFoods");
        }
    }
}
