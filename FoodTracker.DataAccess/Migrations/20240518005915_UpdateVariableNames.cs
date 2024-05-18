using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVariableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Reactions",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Global",
                table: "FoodAliases",
                newName: "IsGlobal");

            migrationBuilder.RenameColumn(
                name: "Vegetarian",
                table: "Food",
                newName: "IsVegetarian");

            migrationBuilder.RenameColumn(
                name: "Vegan",
                table: "Food",
                newName: "IsVegan");

            migrationBuilder.RenameColumn(
                name: "GlutenFree",
                table: "Food",
                newName: "IsGlutenFree");

            migrationBuilder.RenameColumn(
                name: "Global",
                table: "Food",
                newName: "IsGlobal");

            migrationBuilder.RenameColumn(
                name: "Polyols",
                table: "Fodmaps",
                newName: "IsFreeUse");

            migrationBuilder.RenameColumn(
                name: "Oligos",
                table: "Fodmaps",
                newName: "HasPolyols");

            migrationBuilder.RenameColumn(
                name: "Lactose",
                table: "Fodmaps",
                newName: "HasOligos");

            migrationBuilder.RenameColumn(
                name: "Fructose",
                table: "Fodmaps",
                newName: "HasLactose");

            migrationBuilder.RenameColumn(
                name: "FreeUse",
                table: "Fodmaps",
                newName: "HasFructose");

            migrationBuilder.RenameColumn(
                name: "Global",
                table: "FodmapAliases",
                newName: "IsGlobal");

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobal",
                table: "Meals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTemplate",
                table: "Meals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGlobal",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "IsTemplate",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Reactions",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "IsGlobal",
                table: "FoodAliases",
                newName: "Global");

            migrationBuilder.RenameColumn(
                name: "IsVegetarian",
                table: "Food",
                newName: "Vegetarian");

            migrationBuilder.RenameColumn(
                name: "IsVegan",
                table: "Food",
                newName: "Vegan");

            migrationBuilder.RenameColumn(
                name: "IsGlutenFree",
                table: "Food",
                newName: "GlutenFree");

            migrationBuilder.RenameColumn(
                name: "IsGlobal",
                table: "Food",
                newName: "Global");

            migrationBuilder.RenameColumn(
                name: "IsFreeUse",
                table: "Fodmaps",
                newName: "Polyols");

            migrationBuilder.RenameColumn(
                name: "HasPolyols",
                table: "Fodmaps",
                newName: "Oligos");

            migrationBuilder.RenameColumn(
                name: "HasOligos",
                table: "Fodmaps",
                newName: "Lactose");

            migrationBuilder.RenameColumn(
                name: "HasLactose",
                table: "Fodmaps",
                newName: "Fructose");

            migrationBuilder.RenameColumn(
                name: "HasFructose",
                table: "Fodmaps",
                newName: "FreeUse");

            migrationBuilder.RenameColumn(
                name: "IsGlobal",
                table: "FodmapAliases",
                newName: "Global");
        }
    }
}
