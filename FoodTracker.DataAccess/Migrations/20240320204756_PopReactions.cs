using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PopReactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubsidedOn",
                table: "Reactions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IdentifiedOn",
                table: "Reactions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "Active", "AppUserId", "FoodId", "IdentifiedOn", "SeverityId", "SubsidedOn", "TypeId" },
                values: new object[,]
                {
                    { 1, true, "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9", 1, null, 3, null, 1 },
                    { 2, true, "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9", 1, null, 2, null, 2 },
                    { 3, true, "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9", 2, null, 2, null, 4 },
                    { 4, true, "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9", 1, null, 3, null, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reactions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubsidedOn",
                table: "Reactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IdentifiedOn",
                table: "Reactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
