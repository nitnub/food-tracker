using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "AL", "Alabama" },
                    { 2, "AK", "Alaska" },
                    { 3, "AZ", "Arizona" },
                    { 4, "AR", "Arkansas" },
                    { 5, "AS", "American Samoa" },
                    { 6, "CA", "California" },
                    { 7, "CO", "Colorado" },
                    { 8, "CT", "Connecticut" },
                    { 9, "DE", "Delaware" },
                    { 10, "DC", "District of Columbia" },
                    { 11, "FL", "Florida" },
                    { 12, "GA", "Georgia" },
                    { 13, "GU", "Guam" },
                    { 14, "HI", "Hawaii" },
                    { 15, "ID", "Idaho" },
                    { 16, "IL", "Illinois" },
                    { 17, "IN", "Indiana" },
                    { 18, "IA", "Iowa" },
                    { 19, "KS", "Kansas" },
                    { 20, "KY", "Kentucky" },
                    { 21, "LA", "Louisiana" },
                    { 22, "ME", "Maine" },
                    { 23, "MD", "Maryland" },
                    { 24, "MA", "Massachusetts" },
                    { 25, "MI", "Michigan" },
                    { 26, "MN", "Minnesota" },
                    { 27, "MS", "Mississippi" },
                    { 28, "MO", "Missouri" },
                    { 29, "MT", "Montana" },
                    { 30, "NE", "Nebraska" },
                    { 31, "NV", "Nevada" },
                    { 32, "NH", "New Hampshire" },
                    { 33, "NJ", "New Jersey" },
                    { 34, "NM", "New Mexico" },
                    { 35, "NY", "New York" },
                    { 36, "NC", "North Carolina" },
                    { 37, "ND", "North Dakota" },
                    { 38, "MP", "Northern Mariana Islands" },
                    { 39, "OH", "Ohio" },
                    { 40, "OK", "Oklahoma" },
                    { 41, "OR", "Oregon" },
                    { 42, "PA", "Pennsylvania" },
                    { 43, "PR", "Puerto Rico" },
                    { 44, "RI", "Rhode Island" },
                    { 45, "SC", "South Carolina" },
                    { 46, "SD", "South Dakota" },
                    { 47, "TN", "Tennessee" },
                    { 48, "TX", "Texas" },
                    { 49, "TT", "Trust Territories" },
                    { 50, "UT", "Utah" },
                    { 51, "VT", "Vermont" },
                    { 52, "VA", "Virginia" },
                    { 53, "VI", "Virgin Islands" },
                    { 54, "WA", "Washington" },
                    { 55, "WV", "West Virginia" },
                    { 56, "WI", "Wisconsin" },
                    { 57, "WY", "Wyoming" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
