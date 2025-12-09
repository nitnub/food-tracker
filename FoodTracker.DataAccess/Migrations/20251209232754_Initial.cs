using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityIntensities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityIntensities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FodmapCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FodmapCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IconGroupTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IconGroupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "ReactionSeverities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionSeverities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReactionSourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionSourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSafeDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSafeDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HTML = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconGroupTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Icons_IconGroupTypes_IconGroupTypeId",
                        column: x => x.IconGroupTypeId,
                        principalTable: "IconGroupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddressOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddressTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamePlural = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortNamePlural = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_UnitTypes_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "UnitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTypes_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactionCategories_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MealTypeId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    IsTemplate = table.Column<bool>(type: "bit", nullable: false),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Meals_MealTypes_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fodmaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    IsFreeUse = table.Column<bool>(type: "bit", nullable: false),
                    MaxUse = table.Column<int>(type: "int", nullable: false),
                    MaxUseUnitsId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    HasOligos = table.Column<bool>(type: "bit", nullable: false),
                    HasFructose = table.Column<bool>(type: "bit", nullable: false),
                    HasPolyols = table.Column<bool>(type: "bit", nullable: false),
                    HasLactose = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fodmaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fodmaps_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fodmaps_FodmapCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FodmapCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fodmaps_Units_MaxUseUnitsId",
                        column: x => x.MaxUseUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    IntensityId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityIntensities_IntensityId",
                        column: x => x.IntensityId,
                        principalTable: "ActivityIntensities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactionTypes_ReactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ReactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FodmapAliases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FodmapId = table.Column<int>(type: "int", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FodmapAliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FodmapAliases_Fodmaps_FodmapId",
                        column: x => x.FodmapId,
                        principalTable: "Fodmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    FodmapId = table.Column<int>(type: "int", nullable: true),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    IsGlutenFree = table.Column<bool>(type: "bit", nullable: false),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_Fodmaps_FodmapId",
                        column: x => x.FodmapId,
                        principalTable: "Fodmaps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodAliases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodAliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodAliases_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentFoodId = table.Column<int>(type: "int", nullable: true),
                    IngredientFoodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientMaps_Food_IngredientFoodId",
                        column: x => x.IngredientFoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IngredientMaps_Food_ParentFoodId",
                        column: x => x.ParentFoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MealItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    VolumeUnitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealItems_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealItems_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealItems_Units_VolumeUnitsId",
                        column: x => x.VolumeUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: true),
                    SourceTypeId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: true),
                    ActivityId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    SeverityId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubsidedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reactions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reactions_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reactions_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reactions_ReactionSeverities_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "ReactionSeverities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reactions_ReactionSourceTypes_SourceTypeId",
                        column: x => x.SourceTypeId,
                        principalTable: "ReactionSourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reactions_ReactionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ReactionTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSafeFoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSafeFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSafeFoods_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Activity" },
                    { 2, "Meal" }
                });

            migrationBuilder.InsertData(
                table: "IconGroupTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Activity" },
                    { 2, "Admin" },
                    { 3, "FODMAP" },
                    { 4, "Food" },
                    { 5, "Reaction" },
                    { 6, "Site" }
                });

            migrationBuilder.InsertData(
                table: "ReactionSourceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Activity" },
                    { 2, "Day" },
                    { 3, "Food" },
                    { 4, "Meal" }
                });

            migrationBuilder.InsertData(
                table: "States",
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

            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "Id", "HTML", "Name", "IconGroupTypeId" },
                values: new object[,]
                {
                    { 1, "<svg \n                            fill=\"currentColor\" \n                            height=\"100%\" \n                            viewBox=\"0 0 116.04 122.88\" \n                            style=\"enable-background:new 0 0 116.04 122.88\" \n                            xml:space=\"preserve\">\n                            <g>\n                                <path d=\"M63.05,1.84c-0.04,3.61,0.24,7.2,0.87,10.79c0.51,2.88,1.25,5.73,2.23,8.55c3.4-2.37,6.99-4.23,10.7-5.35 c4.02-1.21,8.17-1.54,12.4-0.7c1.25,0.25,2.47,0.6,3.67,1.03c10.01,3.62,16.7,13.2,20.2,25.11c3.43,11.67,3.82,25.63,1.28,38.33 c-0.73,3.67-1.72,7.25-2.96,10.67c-2.06,5.69-4.52,10.46-7.46,14.45c-2.97,4.03-6.41,7.26-10.38,9.83c-0.61,0.39-1.27,0.79-2,1.19 c-6.79,3.78-17.04,6.87-26.8,7.12c-10.02,0.26-19.6-2.44-24.7-10.35c-0.22-0.34-0.45-0.72-0.67-1.13 c-0.61-1.09-1.11-2.14-1.61-3.21c-1.38-2.9-2.79-5.89-5.95-8.51c-1.2-1-2.5-1.7-3.89-2.12c-1.43-0.43-2.96-0.57-4.57-0.42 c-4.79,0.44-7.38,2.36-9.81,4.16c-1.84,1.36-3.59,2.66-6.08,3.42c-0.03,0.01-0.06,0.02-0.09,0.03c-0.31,0.09-0.6,0.16-0.88,0.2 c-0.32,0.05-0.64,0.08-0.96,0.08c-1.79,0-3.19-0.82-4.17-2.53c-0.77-1.36-1.23-3.33-1.35-5.96l0-0.07H0.05 c0-0.92-0.01-1.42-0.03-1.86c-0.1-3.63-0.13-4.84,3.57-8.19c0.56-0.51,1.13-0.97,1.71-1.4c0.61-0.45,1.24-0.86,1.88-1.25 c1.08-0.65,2.24-1.25,3.48-1.81c1.18-0.54,2.48-1.05,3.89-1.56c9.65-3.48,15.35-1.55,20.74,0.28c3.32,1.13,6.51,2.21,10.44,1.67 c3.9-0.53,7.28-3.43,9.77-7.2c3.01-4.55,4.68-10.3,4.45-14.82c-0.38-7.65-3.28-13.92-6.51-20.92c-3.96-8.57-8.41-18.18-9.63-32.78 l-0.33-3.88c-0.08-0.96,0.63-1.8,1.59-1.88c0.05,0,0.11-0.01,0.16-0.01L61.23,0c0.96-0.05,1.77,0.69,1.82,1.65 C63.05,1.71,63.05,1.78,63.05,1.84L63.05,1.84z M94.59,80.46c1.05,0,2,0.43,2.69,1.11c0.69,0.69,1.12,1.64,1.12,2.69 c0,1.05-0.43,2-1.12,2.69s-1.64,1.12-2.69,1.12s-2-0.43-2.69-1.12c-0.69-0.69-1.12-1.64-1.12-2.69c0-1.05,0.43-2,1.12-2.69 C92.58,80.88,93.53,80.46,94.59,80.46L94.59,80.46z M48.35,92.05c1.05,0,2,0.43,2.69,1.12c0.69,0.69,1.11,1.64,1.11,2.69 c0,1.05-0.43,2-1.11,2.69c-0.69,0.69-1.64,1.11-2.69,1.11c-1.05,0-2-0.43-2.69-1.11c-0.69-0.69-1.12-1.64-1.12-2.69 c0-1.05,0.43-2,1.12-2.69C46.35,92.48,47.3,92.05,48.35,92.05L48.35,92.05z M55.82,105.56c1.05,0,2,0.43,2.69,1.12 c0.69,0.69,1.12,1.64,1.12,2.69c0,1.05-0.43,2-1.12,2.69c-0.69,0.69-1.64,1.11-2.69,1.11s-2-0.43-2.69-1.11 c-0.69-0.69-1.12-1.64-1.12-2.69c0-1.05,0.43-2,1.12-2.69C53.82,105.98,54.77,105.56,55.82,105.56L55.82,105.56z M90.61,97.93 c1.05,0,2,0.43,2.69,1.12s1.11,1.64,1.11,2.69s-0.43,2-1.11,2.69c-0.69,0.69-1.64,1.11-2.69,1.11c-1.05,0-2-0.43-2.69-1.11 c-0.69-0.69-1.11-1.64-1.11-2.69s0.43-2,1.11-2.69C88.61,98.36,89.56,97.93,90.61,97.93L90.61,97.93z M75.52,105.08 c1.05,0,2,0.43,2.69,1.12c0.69,0.69,1.12,1.64,1.12,2.69s-0.43,2-1.12,2.69c-0.69,0.69-1.64,1.12-2.69,1.12s-2-0.43-2.69-1.12 c-0.69-0.69-1.12-1.64-1.12-2.69s0.43-2,1.12-2.69C73.52,105.51,74.47,105.08,75.52,105.08L75.52,105.08z M63.61,94.12 c1.05,0,2,0.43,2.69,1.12c0.69,0.69,1.12,1.64,1.12,2.69c0,1.05-0.43,2-1.12,2.69c-0.69,0.69-1.64,1.11-2.69,1.11 c-1.05,0-2-0.43-2.69-1.11c-0.69-0.69-1.12-1.64-1.12-2.69c0-1.05,0.43-2,1.12-2.69C61.6,94.55,62.55,94.12,63.61,94.12 L63.61,94.12z M61.7,80.62c1.05,0,2,0.43,2.69,1.12c0.69,0.69,1.11,1.64,1.11,2.69c0,1.05-0.43,2-1.11,2.69 c-0.69,0.69-1.64,1.11-2.69,1.11c-1.05,0-2-0.43-2.69-1.11s-1.11-1.64-1.11-2.69c0-1.05,0.43-2,1.11-2.69S60.65,80.62,61.7,80.62 L61.7,80.62z M79.18,86.34c1.05,0,2,0.43,2.69,1.11c0.69,0.69,1.11,1.64,1.11,2.69c0,1.05-0.43,2-1.11,2.69s-1.64,1.12-2.69,1.12 c-1.05,0-2-0.43-2.69-1.12c-0.69-0.69-1.11-1.64-1.11-2.69c0-1.05,0.43-2,1.11-2.69C77.17,86.76,78.12,86.34,79.18,86.34 L79.18,86.34z M62.83,66.56c2.73,2.07,6.04,3.98,9.27,4.45c2.45,0.35,4.88-0.16,6.98-2.18c7.07-6.81,12.53-3.9,18.05-0.96 c4,2.13,8.05,4.29,12.71,1.13c0.46-0.31,0.9-0.72,1.33-1.18c0.44-0.47,0.88-1.01,1.32-1.56c0.35-8.31-0.54-16.64-2.71-23.99 c-3.21-10.9-9.2-19.62-18.05-22.82c-1-0.36-2.05-0.66-3.17-0.88c-3.63-0.73-7.23-0.44-10.72,0.61c-3.97,1.19-7.83,3.37-11.46,6.15 l0,0c-0.12,0.09-0.26,0.17-0.41,0.23c-0.89,0.35-1.9-0.08-2.25-0.97c-1.49-3.76-2.55-7.55-3.22-11.35 c-0.56-3.19-0.86-6.41-0.91-9.66L47.11,4.22l0.18,2.1c1.17,14,5.48,23.31,9.32,31.61c3.38,7.31,6.41,13.87,6.83,22.21 C63.54,62.14,63.32,64.32,62.83,66.56L62.83,66.56z M112.09,71.64c-0.1,0.07-0.2,0.15-0.31,0.22c-6.39,4.33-11.37,1.68-16.29-0.94 c-4.4-2.34-8.75-4.66-14.01,0.41c-2.99,2.88-6.43,3.62-9.88,3.12c-3.46-0.5-6.88-2.28-9.79-4.33c-0.84,2.42-1.99,4.8-3.4,6.93 c-3,4.54-7.19,8.04-12.2,8.72c-4.74,0.65-8.31-0.56-12.02-1.82c-4.84-1.64-9.95-3.37-18.46-0.31c-1.25,0.45-2.46,0.93-3.63,1.46 c-1.11,0.5-2.15,1.04-3.11,1.62c-0.58,0.35-1.12,0.71-1.62,1.07c-0.53,0.38-1.01,0.77-1.44,1.17c-2.52,2.28-2.5,3.09-2.43,5.53 c0.02,0.67,0.04,1.43,0.04,1.95h0c0.09,2.02,0.4,3.45,0.89,4.32c0.3,0.53,0.69,0.78,1.15,0.78c0.12,0,0.25-0.01,0.4-0.04 c0.15-0.02,0.29-0.06,0.44-0.1c0.02-0.01,0.04-0.01,0.06-0.02c1.92-0.59,3.44-1.71,5.03-2.89c2.81-2.08,5.81-4.3,11.57-4.83 c2.07-0.19,4.04-0.01,5.88,0.54c1.83,0.55,3.54,1.48,5.12,2.78c3.76,3.12,5.34,6.45,6.87,9.69c0.49,1.04,0.98,2.06,1.5,3 c0.17,0.3,0.35,0.61,0.56,0.93c4.33,6.71,12.77,8.99,21.69,8.76c9.19-0.24,18.82-3.14,25.19-6.69c0.62-0.34,1.22-0.7,1.81-1.08 c3.62-2.33,6.75-5.28,9.46-8.96c2.74-3.72,5.05-8.21,7-13.58c1.16-3.21,2.1-6.63,2.81-10.17 C111.46,76.53,111.83,74.09,112.09,71.64L112.09,71.64z\" />\n                            </g>\n                        </svg>", "Stomach", 5 },
                    { 2, "<svg \n                            fill=\"currentColor\" \n                            shape-rendering=\"geometricPrecision\" \n                            text-rendering=\"geometricPrecision\" \n                            image-rendering=\"optimizeQuality\" \n                            fill-rule=\"evenodd\" \n                            clip-rule=\"evenodd\" \n                            height=\"100%\" \n                            viewBox=\"0 0 512 488.58\">\n                            <path fill-rule=\"nonzero\" d=\"m202.79 10.53 123.29 123.2c21.18 23.62 34.07 38.86 33.91 73.56l152.01 152-17.96 17.96-146.28-146.28c-12.01-12.01-11.87-12.61-10.56-18.47 4.28-18.55-7-43.1-20.01-56.11-43.36-43.36-86.77-86.67-130.13-130.03-13.66-13.67-35.46 8.08-21.82 21.72l45.59 45.59.24.23c1.3 1.31 1.3 3.45 0 4.75l-11.03 11.03a3.35 3.35 0 0 1-4.74 0l-71.56-71.56c-13.44-13.44-35.49 8.06-21.77 21.78l71.55 71.55c1.31 1.31 1.31 3.45 0 4.75l-11.67 11.67a3.362 3.362 0 0 1-4.75 0l-95.7-95.7c-13.5-13.5-35.46 8-21.78 21.68l95.75 95.75a3.362 3.362 0 0 1 0 4.75l-11.39 11.4a3.38 3.38 0 0 1-4.75 0l-71.32-71.32c-13.7-13.71-35.25 8.29-21.72 21.82 17.3 17.3 36.6 36.8 55.92 56.19 16.98 17.18 33.96 34.25 55.72 56.02 3.54 4.01 34.66 32.19 11.74 36.38-16.16 4.45-40.53-.02-53.74 6.1-8.76 4.28-12.94 13.16-7.96 21.72 2.05 3.44 5.54 6.38 10.56 8.24 8.43 3.15 22.44 1.86 31.73 1.86 23.8 0 67.37-.04 98.3-4.25 15.86-2.13 25.04 4.76 34.45 14.17l137.84 137.85-18.04 18.05-147.12-147.12c-29.73 4.43-69.56 3.86-96.92 3.49-15.3-.24-36.57 1.7-50.42-4.09-31.26-13.16-33.22-52.86-3.3-68.81 12.45-6.57 24.51-6.03 38.05-6.43-37.65-37.64-74.97-75.9-112.69-113.6-19.2-19.21-10.04-51.94 14.71-61.13-32.14-32.14 7.8-76.43 40.38-62.66 5.17 2.19 8.92 5.22 12.79 9.11 9.69-24.9 41.67-34.74 61.28-15.13l2.37 2.37C153.06.59 183.1-9.15 202.79 10.53zm240.08 346.15c7.52 0 14.33 3.05 19.24 7.97a27.123 27.123 0 0 1 7.97 19.24c0 7.52-3.05 14.33-7.97 19.24a27.106 27.106 0 0 1-19.24 7.97c-7.51 0-14.33-3.05-19.24-7.97a27.147 27.147 0 0 1-7.97-19.24c0-7.51 3.06-14.33 7.97-19.24a27.123 27.123 0 0 1 19.24-7.97zm10 17.21a14.11 14.11 0 0 0-10-4.14c-3.9 0-7.44 1.59-10 4.14a14.145 14.145 0 0 0 0 20c2.56 2.55 6.1 4.14 10 4.14 3.91 0 7.45-1.59 10-4.14 2.56-2.55 4.14-6.1 4.14-10 0-3.9-1.58-7.44-4.14-10zm-85.71-22.41c7.07 0 12.8 5.73 12.8 12.8s-5.73 12.8-12.8 12.8-12.8-5.73-12.8-12.8 5.73-12.8 12.8-12.8zm-56.36-73.85c7.52 0 14.33 3.05 19.24 7.97a27.123 27.123 0 0 1 7.97 19.24c0 7.52-3.05 14.33-7.97 19.24a27.106 27.106 0 0 1-19.24 7.97c-7.51 0-14.33-3.05-19.24-7.97a27.106 27.106 0 0 1-7.97-19.24c0-7.51 3.05-14.33 7.97-19.24a27.123 27.123 0 0 1 19.24-7.97zm10 17.21a14.11 14.11 0 0 0-10-4.14c-3.9 0-7.44 1.59-10 4.14-2.55 2.55-4.14 6.1-4.14 10 0 3.9 1.59 7.45 4.14 10 2.56 2.55 6.1 4.14 10 4.14 3.91 0 7.45-1.59 10-4.14s4.14-6.1 4.14-10c0-3.9-1.59-7.45-4.14-10zm-35.97-89.02c6.15 0 11.14 4.99 11.14 11.14s-4.99 11.14-11.14 11.14-11.14-4.99-11.14-11.14 4.99-11.14 11.14-11.14zm-50.44 50.17c7.07 0 12.8 5.73 12.8 12.8 0 7.06-5.73 12.8-12.8 12.8s-12.8-5.74-12.8-12.8c0-7.07 5.73-12.8 12.8-12.8zm-62.38-76.04c7.07 0 12.8 5.74 12.8 12.8 0 7.07-5.73 12.8-12.8 12.8s-12.8-5.73-12.8-12.8c0-7.06 5.73-12.8 12.8-12.8zm70.47-49.64c7.51 0 14.32 3.06 19.24 7.97 4.91 4.92 7.97 11.73 7.97 19.24 0 7.52-3.06 14.33-7.97 19.25-4.92 4.91-11.73 7.97-19.24 7.97-7.52 0-14.33-3.06-19.25-7.97-4.91-4.92-7.97-11.73-7.97-19.25 0-7.51 3.06-14.32 7.97-19.24 4.92-4.91 11.73-7.97 19.25-7.97zm9.99 17.21a14.104 14.104 0 0 0-9.99-4.14c-3.91 0-7.45 1.59-10 4.14-2.56 2.56-4.15 6.1-4.15 10 0 3.91 1.59 7.45 4.15 10 2.55 2.56 6.09 4.15 10 4.15 3.9 0 7.44-1.59 9.99-4.15 2.56-2.55 4.15-6.09 4.15-10 0-3.9-1.59-7.44-4.15-10z\" />\n                        </svg>", "Skin", 5 },
                    { 3, "<svg \n                            fill=\"currentColor\" \n                            height=\"100%\" \n                            viewBox=\"0 0 94.88 122.88\">\n                            <path d=\"M19.78,95.62a1.5,1.5,0,0,1-1.15,2.76,12.31,12.31,0,0,0-3.71-.91,17.17,17.17,0,0,0-3,0c0,.77.13,1.53.23,2.28.15,1.16.37,2.36.64,3.58a1.51,1.51,0,0,1-.19,1.11,12.63,12.63,0,0,0-1.45,4.65,3.83,3.83,0,0,0,1,3.09,6.71,6.71,0,0,0,3.56,1.62,26.07,26.07,0,0,0,9-.3,56.84,56.84,0,0,0,10.26-2.82,27.53,27.53,0,0,0,7.19-3.9c1.9-1.52,2.77-4,3.52-6.12l.58-1.61c.91-2.53,2.51-6.93,4.57-8.52a1.49,1.49,0,0,1,1.83,2.36c-1.38,1.06-2.79,4.94-3.6,7.17l-.57,1.58c-.87,2.49-1.9,5.41-4.48,7.47a30.76,30.76,0,0,1-8,4.35q-1.07.41-2.16.78a24.75,24.75,0,0,1-.1,3.22,27.13,27.13,0,0,1-.79,4.3,1.49,1.49,0,1,1-2.88-.77,23.16,23.16,0,0,0,.7-3.81,19.63,19.63,0,0,0,.1-2c-1.94.54-3.86,1-5.67,1.29a28.72,28.72,0,0,1-10.07.3A9.59,9.59,0,0,1,10,114.28a6.67,6.67,0,0,1-1.84-5.37,15.21,15.21,0,0,1,1.59-5.43c-.22-1.07-.41-2.18-.56-3.33s-.28-2.54-.33-3.83l0-.33c-.23-2.42-.26-4.78-.29-7.15v-.68l-3.87-.23a5,5,0,0,1-2.51-.77,4.62,4.62,0,0,1-1.6-1.82A5.25,5.25,0,0,1,0,82.89a8,8,0,0,1,1.39-4.13l4-8.14c2-4,1.94-4.54,1.86-6.65,0-.93-.09-2.14,0-3.94A29.85,29.85,0,0,1,8,53.92a20,20,0,0,1,1.38-4L2,46.75a1.5,1.5,0,0,1-.77-2,1.69,1.69,0,0,1,.19-.32c6.79-8.8,13.87-15,21-19.13l.1,3.44A70.48,70.48,0,0,0,5,44.79L12.2,48a1.51,1.51,0,0,1,.51,2,16,16,0,0,0-1.85,4.62,27.3,27.3,0,0,0-.69,5.5c-.05,1.69,0,2.84,0,3.73.1,2.71.13,3.39-2.16,8.08L3.93,80.31A5.13,5.13,0,0,0,3,83,2.35,2.35,0,0,0,3.19,84a1.58,1.58,0,0,0,.56.64,1.92,1.92,0,0,0,1,.29l4.75.28a1.33,1.33,0,0,1,.45-.08l4.4.36a1.5,1.5,0,0,1-.25,3l-2.6-.16v.47c0,1.91.06,3.8.19,5.7a20.62,20.62,0,0,1,3.52,0,15.77,15.77,0,0,1,4.59,1.11Zm5.9-29.34a2.56,2.56,0,1,0,2.56,2.55,2.55,2.55,0,0,0-2.56-2.55Zm7.76-3.15a1.49,1.49,0,1,1-1.39,2.64,12.18,12.18,0,0,0-5.54-1.26,14.18,14.18,0,0,0-5.91,1.27,1.49,1.49,0,1,1-1.26-2.71,17.22,17.22,0,0,1,7.17-1.54,15.24,15.24,0,0,1,6.93,1.6ZM65.13,93.36a1.49,1.49,0,0,1,3,.3c-.13,1.25-.23,2.47-.29,3.68,16-10.14,20.91-24.85,18.69-38.56a39.47,39.47,0,0,0-1.43-6l2.65-1.5a44.58,44.58,0,0,1,1.72,7c2.45,15.12-3.14,31.32-21.11,42.22a1.48,1.48,0,0,1-.59.2,58.18,58.18,0,0,0,.42,7,53.57,53.57,0,0,0,3,12.34,1.49,1.49,0,1,1-2.79,1.06,56.29,56.29,0,0,1-3.19-13,65.28,65.28,0,0,1-.07-14.68ZM59.42,73.53a1.49,1.49,0,0,1-2.94.54,6.4,6.4,0,0,1,.12-2.84,6.3,6.3,0,0,1,2-3.09A7,7,0,0,1,61.9,66.6a6.88,6.88,0,0,1,3.15.14c2.85.8,4.07,2.24,4.32,4.09A9.52,9.52,0,0,1,68.1,76a71.9,71.9,0,0,1-6.62,11.51c-4.08,5.27-8,7.18-8.65-4A1.49,1.49,0,0,1,54.23,82a1.51,1.51,0,0,1,1.58,1.41c.26,4.87,1.77,4.32,3.31,2.33a70.46,70.46,0,0,0,6.29-11,7.36,7.36,0,0,0,1-3.51c-.09-.65-.7-1.21-2.17-1.63a4,4,0,0,0-1.81-.08,4.08,4.08,0,0,0-1.9.89,3.22,3.22,0,0,0-1.12,3.11ZM70,14.19l-8.23,2L63.78,0,52.33,21.74,60,20.41,57.37,36.67,70,14.19ZM92.43,42.75l-8.51-2.54,11-13.48L72.27,39.92,80,43,68.59,56.24,92.43,42.75ZM40.09,12.17l-6.47,6.41L26.88,2.08l.74,26.84,6.59-5.38,5.91,16.8,0-28.17ZM45.23,19c.9,0,1.81,0,2.7.11l-.15.29A5.11,5.11,0,0,0,47.19,22c-.65,0-1.3-.05-2-.06V19ZM69,26.53A47.83,47.83,0,0,1,75.61,32l-2.67,1.55a44.49,44.49,0,0,0-5.44-4.42L69,26.53Z\" />\n                        </svg>", "Head", 5 },
                    { 4, "<svg \n                            fill=\"currentColor\"\n                            shape-rendering=\"geometricPrecision\" \n                            text-rendering=\"geometricPrecision\" \n                            image-rendering=\"optimizeQuality\" \n                            fill-rule=\"evenodd\" \n                            clip-rule=\"evenodd\" \n                            height=\"100%\" \n                            viewBox=\"0 0 512 282.68\">\n                            <path fill-rule=\"nonzero\" d=\"M3.14 132.9c14.51-17.53 29.53-33.35 44.94-47.39 60.17-54.78 127.69-84 197.43-85.45 69.61-1.46 141.02 24.79 209.14 80.95 18.45 15.21 36.6 32.54 54.3 52 3.82 4.19 4.02 10.42.78 14.81-19.73 27.91-41.98 51.4-65.97 70.56-53.57 42.77-115.96 63.9-179.2 64.29-63.05.39-126.84-19.87-183.44-59.83-28.31-20-54.85-44.93-78.58-74.67-3.65-4.59-3.29-11.1.6-15.27zM256 83.24c32.09 0 58.1 26.01 58.1 58.1s-26.01 58.1-58.1 58.1-58.1-26.01-58.1-58.1c0-5.97.9-11.74 2.57-17.16 4.25 11.15 15.04 19.07 27.68 19.07 16.35 0 29.61-13.26 29.61-29.61 0-12.7-7.98-23.52-19.2-27.73 5.5-1.73 11.36-2.67 17.44-2.67zm107.24-33.52a141.453 141.453 0 0 1 23.1 37.7c6.92 16.67 10.74 34.9 10.74 53.92 0 19.03-3.82 37.26-10.73 53.94a141.479 141.479 0 0 1-30.6 45.8l-1.92 1.89c26.4-9.83 51.79-24.09 75.37-42.91 20.12-16.07 38.96-35.49 55.99-58.27-15-15.93-30.16-30.18-45.38-42.73-25.22-20.8-50.84-37.2-76.57-49.34zm-212.08 185.9c-10.65-11.81-19.33-25.44-25.5-40.32a140.518 140.518 0 0 1-10.74-53.96c0-19.01 3.81-37.22 10.72-53.87 6.85-16.52 16.75-31.46 28.96-44.1-31.5 13.33-61.97 33.25-90.76 59.44-12.7 11.57-25.04 24.3-36.95 38.17 20.74 24.71 43.54 45.64 67.69 62.71 18.19 12.84 37.15 23.5 56.58 31.93zM300.95 32.58c-13.78-5.71-28.98-8.88-44.94-8.88-15.94 0-31.12 3.17-44.93 8.9-14.34 5.95-27.32 14.73-38.23 25.64-10.88 10.89-19.64 23.85-25.6 38.2-5.71 13.79-8.88 28.97-8.88 44.9 0 15.96 3.17 31.17 8.9 44.98a117.654 117.654 0 0 0 25.58 38.19c10.86 10.84 23.84 19.6 38.24 25.57 13.8 5.72 28.98 8.88 44.92 8.88 15.95 0 31.15-3.17 44.96-8.88 14.36-5.93 27.32-14.7 38.2-25.57 10.88-10.88 19.64-23.84 25.57-38.16 5.72-13.85 8.89-29.05 8.89-45.01 0-15.95-3.17-31.14-8.89-44.95-5.93-14.37-14.69-27.33-25.57-38.21-10.86-10.86-23.84-19.63-38.22-25.6z\" />\n                        </svg>", "Vision", 5 },
                    { 5, "<svg \n                            fill=\"currentColor\" \n                            height=\"100%\" \n                            viewBox=\"0 0 122.88 109.08\">\n                            <path d=\"M75.92,25.86a3.31,3.31,0,1,0,5.75,2.22,3.36,3.36,0,0,0-.8-2.16,17.61,17.61,0,0,1,4.49,1.68,1.86,1.86,0,1,0,1.72-3.29,19.36,19.36,0,0,0-8.54-2.4,16.09,16.09,0,0,0-8.82,2.45,1.86,1.86,0,0,0,1.92,3.19,13,13,0,0,1,4.28-1.69ZM45.78,79.67a2.55,2.55,0,0,1,.67,1.48,2.65,2.65,0,0,1-2.34,2.92h0c-2.26.28-5.37-3.81-6.21-5A17,17,0,0,1,35.09,73,18.69,18.69,0,0,1,36.92,60.3c1.51-3.11,6.27-.72,4.74,2.36-3.29,6.51-1.75,12.25,3.75,16.65.12.12.26.24.37.36Zm-25,25.08a2.66,2.66,0,0,1-3.4,4c-.22-.14-.48-.35-.69-.51A46,46,0,0,1,6.45,97.45,37.2,37.2,0,0,1,.8,84.25,37.66,37.66,0,0,1,.59,69.88,47.47,47.47,0,0,1,5.78,55.37c1.62-3.09,6.3-.57,4.68,2.52A41.47,41.47,0,0,0,5.84,70.71,32.36,32.36,0,0,0,6,83.13a32.72,32.72,0,0,0,4.91,11.41A40.48,40.48,0,0,0,20,104a5.7,5.7,0,0,1,.76.71ZM33,90.67a2.66,2.66,0,0,1-3.43,4l-.05,0a34.72,34.72,0,0,1-8.09-8,24.29,24.29,0,0,1-4.12-9.31,24.91,24.91,0,0,1,.12-10.2,35.43,35.43,0,0,1,4.33-10.59c2-3.05,6.33-.1,4.58,2.74a30.54,30.54,0,0,0-3.67,8.88A18.52,18.52,0,0,0,25.8,83.74a29.71,29.71,0,0,0,6.64,6.54,1.92,1.92,0,0,1,.51.39Zm61.52,3.75a37.65,37.65,0,0,1-1.3,11,2.2,2.2,0,0,1-4.25-1.14,33.4,33.4,0,0,0,1-5.59,27,27,0,0,0,.16-3c-2.85.78-5.67,1.42-8.33,1.88a42.39,42.39,0,0,1-14.8.44,14.06,14.06,0,0,1-7.55-3.61,9.87,9.87,0,0,1-2.71-7.89,22.18,22.18,0,0,1,2.34-8c-.33-1.57-.6-3.2-.82-4.88a161.15,161.15,0,0,1-.95-16.63v-1l-5.61-.33H51.6a7.06,7.06,0,0,1-3.7-1.14A6.46,6.46,0,0,1,45.56,52a7.8,7.8,0,0,1-.76-3.6,11.75,11.75,0,0,1,2-6.08l5.85-11.94c2.88-5.89,2.84-6.67,2.72-9.77a53.78,53.78,0,0,1,1.1-14.76,29.85,29.85,0,0,1,2-5.82l4,1.65a25.12,25.12,0,0,0-1.85,5.21,49.41,49.41,0,0,0-1,13.57c.16,4,.2,5-3.18,11.87l-5.86,12a2.59,2.59,0,0,1-.17.33,7.55,7.55,0,0,0-1.4,3.87,3.31,3.31,0,0,0,.32,1.57,2.38,2.38,0,0,0,.82.93,2.83,2.83,0,0,0,1.43.42h.18c4.94.3,9,.41,13.92.83a2.2,2.2,0,0,1-.37,4.39h-.11l-3.71-.22c.07,3.38.09,6.72.36,10.09a14.75,14.75,0,0,0,4.86,1.45,10.23,10.23,0,0,0,5.24-.83,2.2,2.2,0,0,1,1.77,4A14.54,14.54,0,0,1,66.4,72.3a17.44,17.44,0,0,1-4-.92c.06.59.12,1.18.2,1.78.23,1.71.54,3.47.93,5.25A2.18,2.18,0,0,1,63.22,80a18.41,18.41,0,0,0-2.14,6.83,5.63,5.63,0,0,0,1.45,4.53,9.8,9.8,0,0,0,5.24,2.37A37.76,37.76,0,0,0,81,93.34a83.6,83.6,0,0,0,15-4.13,40.94,40.94,0,0,0,10.57-5.74c2.79-2.24,4.07-5.87,5.16-9,.23-.63.44-1.25.54-1.52l.32-.86c1.36-3.72,3.7-10.16,6.72-12.49A2.19,2.19,0,1,1,122,63.09c-2,1.55-4.08,7.24-5.27,10.51-.24.65-.18.5-.32.88-.25.68-.38,1.06-.52,1.45-1.29,3.67-2.79,8-6.58,11a44.64,44.64,0,0,1-11.72,6.37c-1,.4-2.07.78-3.14,1.13Z\" />\n                        </svg>", "Taste / Smell", 5 },
                    { 6, "<svg \n                            fill=\"currentColor\" \n                            height=\"100%\" \n                            viewBox=\"0 0 122.88 110.33\" \n                            style=\"enable-background:new 0 0 122.88 110.33\" \n                            xml:space=\"preserve\">\n                            <g>\n                                <path d=\"M13.9,67.74c1.15,0.54,2.34,0.96,3.59,1.28c1.24,0.32,2.54,0.55,3.87,0.67c1.18,0.11,2.4,0.15,3.65,0.12 c1.08-0.03,2.2-0.11,3.35-0.24c-0.26-1.96-0.39-3.7-0.34-5.26c0.05-1.73,0.33-3.22,0.89-4.53c0.52-1.21,1.26-2.23,2.27-3.08 c0.82-0.7,1.82-1.28,3.03-1.76c-0.79-0.75-1.51-1.43-2.14-2.07c-0.8-0.81-1.46-1.55-1.97-2.27c-1.28-1.79-1.7-3.34-1.08-4.99 c0.59-1.56,2.17-3.15,4.91-5.1c1.41-1,2-1.88,2.17-2.75c0.18-0.88-0.07-1.83-0.34-2.88c-0.15-0.6-0.31-1.22-0.43-1.83 c-0.12-0.59-0.21-1.2-0.25-1.86c-1.03-0.96-2.52-2.26-4.32-3.09c-1.85-0.85-4.05-1.22-6.43-0.21c-0.44,0.18-0.91,0.17-1.31,0.01 c-0.41-0.17-0.75-0.49-0.93-0.92l0,0c-0.18-0.44-0.17-0.9-0.01-1.31c0.17-0.41,0.49-0.75,0.92-0.93l0.01,0 c2.86-1.21,5.46-1.09,7.69-0.38c1.91,0.61,3.55,1.66,4.87,2.7c0.24-0.8,0.58-1.65,1.05-2.55c0.61-1.18,1.44-2.45,2.55-3.83 c0.49-0.6,0.97-1.23,1.43-1.9c0.46-0.67,0.89-1.41,1.28-2.23c0.39-0.83,0.74-1.77,1.02-2.86c0.28-1.1,0.5-2.34,0.63-3.78h0 c0.04-0.47,0.27-0.88,0.61-1.17c0.34-0.28,0.78-0.44,1.25-0.4c0.47,0.04,0.88,0.27,1.17,0.61l0.03,0.04 c0.26,0.33,0.41,0.76,0.37,1.22l-0.01,0.05c-0.15,1.64-0.4,3.07-0.73,4.34c-0.34,1.29-0.75,2.41-1.22,3.41 c-0.47,1.01-0.99,1.88-1.52,2.67c-0.53,0.78-1.08,1.48-1.62,2.16c-1.23,1.53-2.04,2.89-2.55,4.1c-0.49,1.17-0.7,2.21-0.75,3.15 c0.04,0.14,0.06,0.29,0.05,0.44c0,0.15-0.02,0.3-0.06,0.44c0.03,0.54,0.1,1.05,0.2,1.55c0.11,0.55,0.23,1.04,0.36,1.51 c0.42,1.62,0.79,3.08,0.48,4.59c-0.32,1.52-1.32,3.03-3.64,4.68c-1.83,1.3-2.89,2.28-3.33,3.14c-0.37,0.73-0.23,1.4,0.3,2.16 c0.79,1.12,2.24,2.49,4.11,4.25c0.77-1.74,1.69-3.21,2.74-4.41c1.2-1.37,2.57-2.39,4.11-3.08c1.7-0.76,3.57-1.1,5.6-1.03 c2.02,0.06,4.2,0.52,6.53,1.35c0.45,0.16,0.79,0.49,0.98,0.89c0.19,0.39,0.22,0.85,0.07,1.29l-0.02,0.05 c-0.16,0.44-0.49,0.77-0.88,0.95c-0.39,0.19-0.85,0.22-1.29,0.07l-0.04-0.01c-1.94-0.69-3.71-1.08-5.31-1.14 c-1.58-0.07-3,0.17-4.25,0.73c-1.24,0.55-2.34,1.44-3.31,2.66c-0.98,1.25-1.81,2.85-2.49,4.82l0,0c-0.06,0.17-0.15,0.33-0.25,0.48 c-0.09,0.12-0.2,0.23-0.32,0.32l-0.02,0.03c-0.01,0.02-0.04,0.05-0.08,0.09c-0.12,0.13-0.26,0.23-0.4,0.31 c-0.13,0.07-0.26,0.13-0.4,0.16c-0.02,0.01-0.05,0.02-0.08,0.02l-0.01,0c-1.66,0.37-2.93,0.86-3.89,1.49 c-0.93,0.61-1.55,1.35-1.94,2.25c-0.43,1-0.62,2.24-0.63,3.73c-0.01,1.53,0.18,3.32,0.49,5.41l0,0.02 c0.01,0.05,0.01,0.11,0.01,0.17c0.9,1.13,1.95,2.09,3.12,2.91c1.23,0.86,2.6,1.56,4.1,2.13c1.61,0.62,3.36,1.08,5.23,1.41 c1.87,0.33,3.86,0.54,5.95,0.65c0.1,0,0.19,0.02,0.28,0.04l0,0c0.09,0.02,0.18,0.04,0.26,0.08c1.09,0.34,2.4,0.43,3.89,0.31 c1.53-0.12,3.23-0.46,5.06-0.95c1.93-0.52,3.98-1.22,6.1-2.02c2.12-0.8,4.33-1.72,6.56-2.68l0.03-0.01 c0.05-0.02,0.11-0.04,0.16-0.06c0.07-0.02,0.13-0.04,0.19-0.05L85,69.12V62.1c0-0.03,0-0.07,0.01-0.1l0-0.05 c0.1-1.15,0.3-2.19,0.59-3.1c0.3-0.93,0.69-1.73,1.18-2.41c0.53-0.74,1.17-1.33,1.91-1.77c0.73-0.43,1.56-0.71,2.48-0.83 c0.06-0.01,0.1-0.02,0.14-0.02l0.01,0c0.05,0,0.09-0.01,0.13-0.01l1.29,0.02v0l0.05,0c2.79,0.04,5.36,0.07,7.3-2.12 c1-1.12,1.64-2.3,2.01-3.52c0.37-1.22,0.46-2.48,0.34-3.75c-0.12-1.34-0.47-2.69-0.95-4.04c-0.49-1.36-1.13-2.69-1.83-3.98 c-0.23-0.42-0.26-0.89-0.14-1.31c0.12-0.42,0.41-0.8,0.83-1.02l0.01,0c0.41-0.22,0.88-0.26,1.3-0.13c0.42,0.12,0.8,0.41,1.02,0.83 l0,0c0.79,1.44,1.51,2.97,2.07,4.53c0.57,1.58,0.97,3.2,1.12,4.83c0.15,1.7,0.03,3.4-0.47,5.06c-0.5,1.66-1.38,3.27-2.73,4.79 c-2.99,3.36-6.33,3.31-9.95,3.26v0l-0.05,0c-0.23,0-0.49-0.01-1.1-0.01c-0.43,0.06-0.81,0.19-1.14,0.39 c-0.33,0.2-0.62,0.46-0.87,0.8c-0.29,0.4-0.53,0.91-0.72,1.51c-0.19,0.62-0.32,1.33-0.4,2.15v6.12l10.93-2.82l0,0l0.02,0 c0.02-0.01,0.04-0.01,0.07-0.02l13-2.54c0.63-0.41,1.22-0.85,1.76-1.31c0.58-0.49,1.1-0.99,1.56-1.51 c1.43-1.59,2.39-3.37,2.97-5.27c0.62-2.05,0.79-4.25,0.59-6.49c-0.19-2.17-0.72-4.38-1.52-6.55c-0.85-2.32-1.99-4.59-3.34-6.73 c-0.94-1.49-1.99-2.92-3.1-4.25c-1.13-1.34-2.32-2.58-3.54-3.68c-0.1-0.09-0.19-0.19-0.26-0.29c-0.07-0.1-0.13-0.2-0.17-0.31 l-0.01-0.03c-1.07-2.65-2.27-5.18-3.74-7.47c-1.46-2.28-3.2-4.32-5.34-6.01c-2.14-1.69-4.7-3.04-7.84-3.94 c-3.16-0.91-6.9-1.37-11.36-1.26c-0.11,0-0.21,0-0.3-0.02c-0.09-0.01-0.18-0.04-0.27-0.06l-0.16-0.05 c-0.85,0.59-1.6,1.21-2.25,1.87c-0.7,0.7-1.28,1.42-1.76,2.18c-0.48,0.77-0.86,1.57-1.14,2.4c-0.28,0.84-0.46,1.71-0.54,2.61 c-0.04,0.43-0.23,0.8-0.52,1.08c-0.28,0.27-0.64,0.44-1.04,0.47c-0.03,0-0.06,0.01-0.09,0.01h-0.01c-1.55,0.09-2.91,0.42-4.09,0.96 c-1.25,0.57-2.31,1.37-3.19,2.37c-0.92,1.03-1.66,2.27-2.24,3.68c-0.61,1.46-1.05,3.1-1.33,4.86c0.19,0.46,0.34,0.94,0.46,1.43 c0.13,0.52,0.23,1.05,0.34,1.58c0.16,0.84,0.33,1.68,0.6,2.34c0.24,0.58,0.57,1.02,1.08,1.19c0.45,0.15,0.8,0.46,0.99,0.86 c0.2,0.39,0.25,0.86,0.1,1.31c-0.15,0.45-0.47,0.8-0.86,0.99c-0.39,0.2-0.86,0.25-1.31,0.1l-0.04-0.02 c-1.47-0.5-2.34-1.42-2.91-2.54c-0.55-1.09-0.79-2.33-1.03-3.58c-0.06-0.32-0.12-0.63-0.2-0.99l-0.01-0.04 c-0.05-0.24-0.11-0.48-0.18-0.7c-1.66-1.25-3.57-0.82-5.42,0.29v0c-1.99,1.19-3.94,3.14-5.5,4.7c-0.33,0.33-0.65,0.65-0.95,0.94 c-0.3,0.29-0.59,0.57-0.89,0.84c-0.35,0.32-0.8,0.46-1.24,0.44c-0.43-0.02-0.85-0.21-1.16-0.55l0,0c-0.02-0.02-0.04-0.04-0.05-0.06 c-0.29-0.35-0.42-0.78-0.4-1.2c0.02-0.43,0.21-0.85,0.55-1.16l0,0c0.02-0.02,0.04-0.03,0.06-0.05c0.44-0.4,1.02-0.98,1.65-1.61 c0-0.01,0.03-0.03,0.04-0.04c1.71-1.72,3.8-3.81,6.11-5.23c2.22-1.36,4.63-2.08,7.07-1.25c0.36-1.68,0.86-3.25,1.5-4.69 c0.69-1.54,1.56-2.93,2.62-4.12c1.09-1.22,2.37-2.23,3.85-2.98c1.31-0.66,2.79-1.13,4.44-1.35c0.16-0.84,0.38-1.66,0.68-2.46 c0.34-0.92,0.78-1.8,1.32-2.65c0.45-0.72,0.98-1.41,1.57-2.07c0.39-0.44,0.81-0.86,1.26-1.27c-1.68-0.44-3.35-0.85-5.01-1.22 c-1.91-0.42-3.8-0.78-5.67-1.07l-0.07,0.03c-0.62,0.23-1.2,0.68-1.73,1.29c-0.62,0.71-1.18,1.64-1.67,2.66 c-0.65,1.36-1.16,2.87-1.54,4.31c-0.49,1.84-0.77,3.59-0.87,4.82c-0.04,0.47-0.27,0.89-0.6,1.17c-0.33,0.28-0.77,0.44-1.23,0.4 l-0.04,0c-0.46-0.04-0.87-0.27-1.15-0.6c-0.28-0.33-0.44-0.77-0.4-1.23l0-0.04c0.12-1.38,0.44-3.35,0.99-5.42 c0.43-1.64,1.01-3.35,1.75-4.9c0.36-0.75,0.75-1.47,1.2-2.13c0.22-0.33,0.44-0.64,0.68-0.93c-0.85-0.07-1.69-0.12-2.54-0.16 c-1.17-0.05-2.34-0.05-3.5-0.02c-4.09,0.12-8.15,0.76-12.21,2.09c-3.89,1.28-7.77,3.19-11.69,5.91c0.45,0.29,0.87,0.61,1.28,0.94 c0.61,0.51,1.17,1.06,1.68,1.65c0.71,0.83,1.3,1.75,1.76,2.72c0.45,0.95,0.76,1.95,0.92,2.98c0.07,0.47-0.06,0.92-0.32,1.28 c-0.26,0.35-0.66,0.61-1.13,0.68c-0.47,0.07-0.92-0.06-1.28-0.32c-0.35-0.26-0.61-0.66-0.68-1.13c-0.11-0.71-0.33-1.4-0.64-2.06 c-0.32-0.68-0.75-1.33-1.26-1.93c-0.52-0.61-1.13-1.17-1.82-1.66c-0.66-0.47-1.38-0.88-2.15-1.19c-0.78,0.42-1.55,0.85-2.29,1.3 c-0.82,0.49-1.62,1.01-2.39,1.54c-1.08,0.75-2.11,1.56-3.09,2.44c-0.85,0.76-1.65,1.57-2.39,2.44c1.92,1.57,2.68,3.24,2.82,4.99 c0.15,1.82-0.38,3.69-0.94,5.65c0,0.02-0.01,0.04-0.02,0.07c-1.01,3.57-2.13,7.57,2.81,11.79c0.36,0.31,0.56,0.74,0.6,1.17 c0.03,0.44-0.1,0.89-0.41,1.25c-0.31,0.36-0.74,0.56-1.17,0.6c-0.44,0.03-0.89-0.1-1.25-0.41c-3.31-2.82-4.6-5.54-4.9-8.13 c-0.3-2.57,0.38-4.97,1.02-7.23c0.42-1.48,0.82-2.89,0.75-4.14c-0.06-1.08-0.49-2.1-1.6-3.02c-1.48,1.67-2.85,3.4-4.07,5.23 c-1.3,1.95-2.41,4.02-3.29,6.24c-0.98,2.49-1.66,5.19-1.95,8.14c-0.29,2.96-0.19,6.19,0.38,9.75c0.3,1.88,0.64,3.55,1.08,5.04 c0.44,1.46,0.98,2.76,1.7,3.91c0.7,1.12,1.59,2.13,2.75,3.07C10.68,66.04,12.12,66.91,13.9,67.74L13.9,67.74z M86.82,42.45 c2.21,2.05,2.91,3.61,2.65,5.16c-0.26,1.56-1.5,2.97-3.15,4.87l0,0c-0.78,0.89-1.67,1.92-2.52,3.07c-0.28,0.38-0.69,0.61-1.13,0.68 c-0.43,0.06-0.89-0.04-1.27-0.32c-0.38-0.28-0.61-0.69-0.68-1.13c-0.06-0.43,0.04-0.89,0.32-1.27c1-1.34,1.9-2.38,2.68-3.28 l0.01-0.01c1-1.15,1.75-2.01,1.93-2.8c0.15-0.67-0.12-1.37-1.05-2.32l-0.37,0.14v0c-0.2,0.07-0.49,0.18-0.83,0.3 c-0.32,0.11-0.67,0.24-1.03,0.37c-0.38,0.14-0.8,0.31-1.23,0.49c-0.17,0.07-0.32,0.14-0.47,0.21l-0.48,0.22 c-0.36,0.17-0.73,0.34-1.1,0.5c-0.39,0.16-0.78,0.31-1.18,0.43c-0.53,0.16-1.09,0.34-1.56,0.49l-0.03,0.01 c-1.28,0.4-1.96,0.62-2.9,0.66c-0.5,0.02-1-0.02-1.63-0.13c-0.62-0.1-1.34-0.26-2.33-0.47l-0.95-0.2 c-0.03-0.01-0.05-0.01-0.08-0.02l-0.1-0.03l-3.98-1.33c-0.03-0.01-0.05-0.01-0.07-0.02c-0.44-0.15-0.77-0.46-0.97-0.85 c-0.19-0.38-0.24-0.84-0.11-1.28l0.02-0.06c0.15-0.44,0.46-0.77,0.85-0.97c0.39-0.19,0.84-0.24,1.28-0.11 c0.03,0.01,0.05,0.02,0.07,0.03l3.85,1.28l0.9,0.19c0.88,0.19,1.53,0.33,2.04,0.41c0.48,0.08,0.81,0.12,1.08,0.11 c0.43-0.02,0.97-0.19,1.98-0.51c0.25-0.07,0.49-0.16,0.74-0.23c0.3-0.09,0.61-0.19,0.91-0.28c0.61-0.18,1.21-0.46,1.81-0.74 l0.52-0.24c0.18-0.08,0.36-0.16,0.55-0.24c1.06-0.45,1.83-0.72,2.45-0.94c0.62-0.22,0.94-0.33,1.02-0.55 c0.13-0.32,0.08-1-0.03-2.36c-0.12-1.56-0.17-2.8-0.09-3.87c0.08-1.1,0.31-2.01,0.76-2.87c0.44-0.85,1.05-1.58,1.91-2.31 c0.83-0.71,1.89-1.43,3.24-2.29L89.15,28c0.42-0.27,0.74-0.46,1.01-0.63l0.05-0.03c0.77-0.47,1.21-0.74,1.42-1.12 c0.23-0.4,0.29-1.04,0.31-2.26c0.01-0.71,0.04-1.36,0.13-1.98c0.09-0.65,0.24-1.25,0.5-1.82c0.3-0.64,0.71-1.2,1.28-1.68 c0.56-0.47,1.28-0.85,2.19-1.14c0.45-0.14,0.92-0.09,1.31,0.11c0.39,0.2,0.7,0.56,0.85,1.01c0.14,0.45,0.09,0.92-0.11,1.31 c-0.2,0.39-0.56,0.7-1.01,0.85c-0.42,0.13-0.73,0.29-0.96,0.46c-0.2,0.15-0.34,0.33-0.43,0.52c-0.12,0.26-0.2,0.6-0.24,0.99 c-0.05,0.43-0.07,0.91-0.08,1.43c-0.04,2.07-0.16,3.17-0.66,4.02c-0.51,0.87-1.31,1.36-2.74,2.24l-0.51,0.31l-0.45,0.28l-0.1,0.07 c-1.14,0.73-2.02,1.31-2.67,1.84c-0.62,0.5-1.03,0.95-1.27,1.42c-0.24,0.47-0.36,1.04-0.4,1.8c-0.04,0.8,0.01,1.82,0.11,3.11 c0.06,0.82,0.11,1.49,0.14,2.1C86.84,41.71,86.84,42.11,86.82,42.45L86.82,42.45z M67.07,67.85c0.36,0.28,0.58,0.67,0.65,1.09 c0.07,0.43-0.02,0.89-0.3,1.28l-0.01,0.01c-0.28,0.38-0.68,0.61-1.11,0.68c-0.43,0.07-0.89-0.02-1.28-0.3l0,0 c-0.25-0.18-0.48-0.37-0.69-0.58c-0.22-0.21-0.42-0.44-0.6-0.68c-0.54-0.71-0.93-1.53-1.16-2.41c-0.24-0.91-0.32-1.89-0.25-2.86 c0.06-0.92,0.26-1.86,0.58-2.75c0.31-0.87,0.74-1.71,1.28-2.46l0,0c0.25-0.34,0.52-0.67,0.81-0.98c0.29-0.3,0.59-0.57,0.92-0.82 L65.99,57c0.48-0.36,0.99-0.68,1.53-0.96c0.54-0.28,1.11-0.52,1.7-0.72c0.6-0.2,1.22-0.37,1.87-0.5c0.64-0.13,1.31-0.22,2-0.28 l0.14-0.01h6c0.48,0,0.91,0.19,1.22,0.5l0.03,0.03c0.29,0.31,0.47,0.73,0.47,1.18c0,0.48-0.19,0.91-0.5,1.22 c-0.31,0.31-0.74,0.5-1.22,0.5h-5.95c-0.53,0.05-1.04,0.12-1.54,0.22c-0.5,0.1-0.98,0.22-1.42,0.37c-0.42,0.14-0.83,0.31-1.21,0.51 c-0.37,0.19-0.72,0.4-1.04,0.64L68,59.79c-0.19,0.15-0.37,0.31-0.54,0.48c-0.18,0.18-0.34,0.38-0.49,0.59 c-0.35,0.48-0.63,1.03-0.84,1.61c-0.21,0.59-0.34,1.21-0.38,1.82l0,0.01c-0.04,0.59,0,1.16,0.13,1.67 c0.12,0.48,0.31,0.91,0.59,1.27l0.02,0.03c0.08,0.1,0.16,0.19,0.25,0.28l0,0c0.09,0.09,0.19,0.17,0.3,0.25L67.07,67.85L67.07,67.85 z M40.58,65.49c-0.45,0.11-0.9,0.03-1.28-0.2l0,0c-0.38-0.23-0.67-0.6-0.79-1.06c-0.12-0.46-0.03-0.92,0.19-1.3l0,0 c0.23-0.38,0.6-0.67,1.06-0.79c3.91-0.99,6.27-1.06,8.13-0.4c1.88,0.67,3.15,2.04,4.93,3.94l0.2,0.22c0.73,0.78,1.33,1.41,1.87,1.9 c0.51,0.47,0.97,0.82,1.42,1.05c0.43,0.22,0.9,0.36,1.49,0.44c0.62,0.09,1.36,0.11,2.28,0.1h0c0.47-0.01,0.9,0.18,1.22,0.48 c0.31,0.3,0.51,0.73,0.52,1.21v0.01h0l0,0.05c-0.01,0.45-0.19,0.87-0.48,1.17c-0.3,0.31-0.73,0.51-1.2,0.52h-0.01v0l-0.04,0 c-1.18,0.02-2.14-0.02-2.98-0.16c-0.88-0.14-1.64-0.38-2.37-0.76c-0.71-0.36-1.35-0.83-2.03-1.44c-0.66-0.59-1.36-1.32-2.2-2.22 l-0.2-0.22c-1.33-1.43-2.29-2.46-3.6-2.92c-1.33-0.46-3.1-0.39-6.09,0.37L40.58,65.49L40.58,65.49z M77.6,23.75 c-0.11-0.31-0.24-0.6-0.39-0.88c-0.2-0.36-0.44-0.69-0.69-0.97c-0.28-0.32-0.58-0.59-0.87-0.81c-0.31-0.23-0.61-0.4-0.88-0.51l0,0 c-0.44-0.17-0.77-0.51-0.95-0.91c-0.18-0.4-0.2-0.87-0.03-1.31l0,0c0.17-0.44,0.51-0.77,0.91-0.95c0.4-0.18,0.87-0.2,1.31-0.03 c0.52,0.2,1.08,0.51,1.63,0.91c0.5,0.37,1,0.82,1.46,1.35c0.28,0.32,0.55,0.67,0.79,1.04c0.14,0.21,0.27,0.43,0.39,0.65 c0.23-0.11,0.47-0.22,0.74-0.34l0.03-0.01c0.43-0.19,0.91-0.39,1.45-0.61c0.54-0.22,1.14-0.47,1.83-0.77 c0.68-0.3,1.44-0.64,2.3-1.06c0.43-0.21,0.9-0.22,1.31-0.07c0.42,0.14,0.78,0.45,0.98,0.88c0.21,0.43,0.22,0.9,0.07,1.31 c-0.14,0.42-0.45,0.78-0.88,0.98c-0.87,0.42-1.67,0.79-2.39,1.1c-0.69,0.3-1.32,0.56-1.89,0.8c-0.02,0.01-0.05,0.02-0.08,0.03 c-2.72,1.13-3.31,1.39-3.6,4.54l-0.01,0.11c-0.18,1.89-0.31,3.36-0.83,4.76c-0.52,1.42-1.41,2.71-3.08,4.2 c-0.35,0.32-0.8,0.46-1.24,0.43c-0.44-0.03-0.87-0.22-1.19-0.57c-0.32-0.35-0.46-0.8-0.43-1.24c0.03-0.44,0.22-0.87,0.57-1.19 c1.18-1.05,1.8-1.97,2.16-2.98c0.37-1.04,0.48-2.21,0.62-3.71l0.01-0.11c0.09-0.99,0.2-1.8,0.35-2.47 C77.22,24.71,77.39,24.19,77.6,23.75L77.6,23.75z M4.86,45.88c0-0.02,0-0.05,0-0.07c0.03-0.45,0.23-0.86,0.54-1.15 c0.32-0.3,0.75-0.48,1.21-0.46v0c0.02,0,0.05,0,0.07,0c0.45,0.03,0.86,0.23,1.15,0.54c0.3,0.32,0.48,0.76,0.46,1.23 c-0.06,1.82,0.52,2.94,1.45,3.73c0.98,0.83,2.38,1.34,3.86,1.88c2.21,0.81,4.55,1.66,6.46,3.42c1.92,1.77,3.37,4.41,3.71,8.74 c0.04,0.47-0.12,0.92-0.41,1.25c-0.29,0.34-0.7,0.56-1.17,0.6c-0.47,0.04-0.92-0.12-1.25-0.41c-0.34-0.29-0.56-0.7-0.6-1.17 c-0.26-3.26-1.35-5.23-2.8-6.54l-0.03-0.03c-1.46-1.31-3.33-1.99-5.09-2.63c-2.01-0.73-3.91-1.43-5.32-2.71 C5.66,50.79,4.77,48.92,4.86,45.88L4.86,45.88L4.86,45.88z M85.28,72.59c0.68,0.3,1.37,0.54,2.09,0.73 c1.15,0.3,2.34,0.48,3.57,0.53c1.69,0.07,3.46-0.08,5.3-0.42c1.85-0.34,3.77-0.87,5.75-1.56c0.45-0.16,0.92-0.12,1.31,0.08 c0.4,0.19,0.72,0.53,0.87,0.98c0.16,0.45,0.12,0.92-0.08,1.31c-0.19,0.4-0.53,0.72-0.98,0.87c-2.16,0.76-4.28,1.34-6.33,1.71 c-2.06,0.37-4.06,0.54-5.99,0.46c-1.91-0.08-3.75-0.4-5.49-0.99c-1.68-0.57-3.28-1.38-4.77-2.47l-5.46,1.41 c1.13,1.38,2.32,2.56,3.59,3.54c1.44,1.11,2.98,1.94,4.63,2.49c1.8,0.59,3.75,0.84,5.85,0.71c2.12-0.13,4.4-0.65,6.85-1.57 c0.44-0.17,0.91-0.14,1.31,0.04c0.4,0.18,0.73,0.52,0.9,0.96c0.17,0.44,0.14,0.91-0.04,1.31c-0.18,0.4-0.52,0.73-0.96,0.9 c-2.82,1.07-5.47,1.66-7.96,1.8c-2.5,0.14-4.84-0.16-7.02-0.88c-2.09-0.69-4.03-1.76-5.83-3.17c-1.69-1.33-3.26-2.97-4.71-4.89 c-0.49,0.2-0.99,0.41-1.51,0.62c-0.41,0.17-0.83,0.34-1.26,0.51c1.12,1.61,2.29,3.04,3.5,4.29c1.34,1.39,2.72,2.55,4.14,3.48 c1.29,0.84,2.59,1.49,3.91,1.92c1.28,0.42,2.56,0.65,3.84,0.67l0.11-0.02l0.04,0c0.05-0.01,0.09-0.01,0.13-0.01 c4.35-0.23,7.99-0.77,10.98-1.69c2.96-0.91,5.3-2.19,7.08-3.92c1.67-1.63,2.88-3.68,3.7-6.24c0.78-2.44,1.19-5.33,1.3-8.73 l-7.54,1.47L85.28,72.59L85.28,72.59z M86.57,91.21l1.75,15.59c0.05,0.46-0.1,0.91-0.37,1.25c-0.27,0.34-0.68,0.58-1.15,0.63 l-0.02,0c-0.46,0.05-0.91-0.1-1.25-0.37c-0.34-0.27-0.58-0.68-0.63-1.15l-1.78-15.84c-1.37-0.12-2.73-0.42-4.07-0.89 c-1.49-0.52-2.96-1.26-4.39-2.2c-1.67-1.1-3.31-2.47-4.87-4.11c-1.47-1.54-2.87-3.31-4.19-5.29c-0.77,0.28-1.54,0.54-2.29,0.78 c-0.89,0.29-1.75,0.54-2.58,0.77l-0.24,0.06c1.57,2.8,3.3,4.92,5.06,6.65c1.88,1.85,3.8,3.25,5.58,4.54 c2.64,1.93,5.01,3.66,6.7,6.18c1.7,2.54,2.68,5.83,2.51,10.85c-0.02,0.47-0.22,0.9-0.54,1.2c-0.32,0.3-0.76,0.48-1.23,0.46 c-0.47-0.01-0.9-0.22-1.2-0.54c-0.3-0.32-0.48-0.76-0.46-1.23c0.14-4.14-0.67-6.85-2.07-8.92c-1.41-2.09-3.45-3.58-5.73-5.23 c-1.97-1.44-4.1-2.99-6.2-5.09c-2.06-2.06-4.09-4.63-5.92-8.12c-1.26,0.2-2.47,0.31-3.6,0.29c-1.24-0.01-2.38-0.17-3.43-0.48 c-2.2-0.12-4.31-0.34-6.31-0.71c-2.03-0.37-3.94-0.88-5.71-1.55c-1.72-0.66-3.32-1.47-4.76-2.48c-1.37-0.95-2.61-2.06-3.69-3.36 c-1.42,0.18-2.81,0.3-4.18,0.34c-1.46,0.04-2.89,0-4.28-0.13c-1.52-0.14-3-0.4-4.43-0.77c-1.43-0.37-2.82-0.87-4.16-1.49 c-2.1-0.98-3.8-2.02-5.2-3.16c-1.41-1.16-2.52-2.42-3.41-3.84c-0.87-1.39-1.52-2.91-2.03-4.61c-0.5-1.67-0.88-3.53-1.22-5.61 c-0.63-3.88-0.73-7.42-0.41-10.67c0.32-3.27,1.08-6.25,2.17-9.01c1.08-2.75,2.48-5.27,4.12-7.62c1.63-2.35,3.48-4.53,5.46-6.62 c0.98-1.23,2.05-2.35,3.18-3.39c1.13-1.03,2.34-1.98,3.6-2.87c1.24-0.86,2.53-1.66,3.86-2.41c1.32-0.75,2.67-1.44,4.03-2.09 c4.37-3.09,8.72-5.25,13.07-6.67c4.36-1.43,8.73-2.11,13.13-2.24c4.34-0.13,8.7,0.29,13.12,1.07c4.39,0.77,8.84,1.9,13.39,3.21 c4.81-0.09,8.87,0.43,12.33,1.46c3.5,1.03,6.39,2.57,8.82,4.49c2.41,1.9,4.34,4.15,5.97,6.64c1.6,2.45,2.89,5.11,4.03,7.89 c1.28,1.17,2.51,2.47,3.68,3.87c1.18,1.42,2.28,2.93,3.28,4.51c1.49,2.36,2.75,4.88,3.69,7.45c0.89,2.44,1.48,4.92,1.69,7.37 c0.24,2.69,0.02,5.34-0.75,7.85c-0.72,2.33-1.92,4.52-3.69,6.5c-0.6,0.66-1.25,1.3-1.96,1.89c-0.69,0.58-1.44,1.13-2.26,1.65 c-0.1,0.07-0.21,0.14-0.33,0.19c-0.11,0.05-0.23,0.09-0.36,0.11l-2.35,0.46c-0.07,4.06-0.55,7.53-1.49,10.48 c-0.99,3.08-2.48,5.6-4.57,7.63c-2.02,1.97-4.57,3.44-7.71,4.5C94.3,90.26,90.74,90.89,86.57,91.21L86.57,91.21z\" />\n                            </g>\n                        </svg>", "Nervous", 5 },
                    { 7, "<svg \n                            fill=\"currentColor\" \n                            class=\"bi bi-emoji-laughing\" \n                            height=\"100%\" \n                            viewBox=\"0 0 16 16\">\n                            <path d=\"M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16\" />\n                            <path d=\"M12.331 9.5a1 1 0 0 1 0 1A5 5 0 0 1 8 13a5 5 0 0 1-4.33-2.5A1 1 0 0 1 4.535 9h6.93a1 1 0 0 1 .866.5M7 6.5c0 .828-.448 0-1 0s-1 .828-1 0S5.448 5 6 5s1 .672 1 1.5m4 0c0 .828-.448 0-1 0s-1 .828-1 0S9.448 5 10 5s1 .672 1 1.5\" />\n                        </svg>", "Feeling Good!", 5 },
                    { 8, "<svg xmlns = \"http://www.w3.org/2000/svg\" fill=\"currentColor\" class=\"bi bi-emoji-smile\" height=\"100%\" viewBox=\"0 0 16 16\">\n                            <path d = \"M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16\" />\n                            <path d=\"M4.285 9.567a.5.5 0 0 1 .683.183A3.5 3.5 0 0 0 8 11.5a3.5 3.5 0 0 0 3.032-1.75.5.5 0 1 1 .866.5A4.5 4.5 0 0 1 8 12.5a4.5 4.5 0 0 1-3.898-2.25.5.5 0 0 1 .183-.683M7 6.5C7 7.328 6.552 8 6 8s-1-.672-1-1.5S5.448 5 6 5s1 .672 1 1.5m4 0c0 .828-.448 1.5-1 1.5s-1-.672-1-1.5S9.448 5 10 5s1 .672 1 1.5\" />\n                        </svg>", "No Reactions", 5 },
                    { 9, "<svg height=\"100%\" viewBox=\"0 0 102.16 122.88\">\n                            <path d=\"M42.08,68.7a68,68,0,0,0,7.74,4c7.53,3.51,13.72,6.4,15.05,18.23a36.81,36.81,0,0,1,0,6.3v0c0,.32,0,.66-.07,1.46l-.8,16.8c7.24,0,14.23.23,20.39.6,1.39-.32,3.35-1,5.42-1.52L73.63,54.26l-2.39.94h0l-.89.32c-5.19,1.84-8.95,3.17-15.53,1.55a.34.34,0,0,1-.14,0c-4.6-1.78-7-4.26-9.31-6.69L45,50l-3,18.73ZM9.61,29.51l8.36.6c2.12.15,3.49,2.64,3.11,4.93L16.57,62.29c-.36,2.19-10.93,4-13.4,4.62C1.11,67.45-.33,64.27.07,62L5.12,32.92a4.11,4.11,0,0,1,4.49-3.41Zm41,86.17c.13-6.57.35-10.93.72-18l0-1.07a31,31,0,0,0,.07-3.43,8,8,0,0,0-.67-2.88L50.7,90c-.47-1-.9-2-1.54-2.27a54.32,54.32,0,0,0-5.65-2.11c-2.66-.87-5.62-1.76-8.24-2.5C33.35,89.51,30.74,97.2,28,104.52c-2.14,5.6-5,9.15-7.26,13,8.43-1,19.06-1.62,30-1.88ZM5,120.94c2.45-6,6.93-13.38,9.69-21.29,2.84-8.13,5.56-16.42,7.56-23.08a21.28,21.28,0,0,1-3.11-4.89,10.93,10.93,0,0,1-.84-6.45L22,45.44C23.46,34.8,23.74,29.06,35,29.81c10.63.69,15.61,6.29,19.47,10.63,1.71,1.92,3.19,3.58,4.78,4.34.75.36,1.94-.14,3.28-.71a22,22,0,0,1,2.59-1l1.16-.44.67-.25,3.14-1.25-1.29-4.83a3.41,3.41,0,0,1,6.58-1.76l1.09,4.06.15-.06h0a5.06,5.06,0,0,1,4.13.61,7.24,7.24,0,0,1,2.4,2.37,6.58,6.58,0,0,1,1,3.52,8.79,8.79,0,0,1-3,6.1.84.84,0,0,1-.31.19l-.94.37L96.7,114a4.3,4.3,0,0,1,2.31,1.2,14.92,14.92,0,0,1,2.67,4.17l.48,3.52h-99c0-.7.67-1.35,1.85-1.94ZM35.88.69a12.74,12.74,0,1,1-7.33,6.45A12.7,12.7,0,0,1,35.88.69Z\"/>\n                        </svg>", "Hiking", 1 },
                    { 10, "<svg height=\"100%\" viewBox=\"0 0 122.88 113.11\">\n                            <g>\n                                <path class=\"st0\" d=\"M99.83,67.01c12.72,0,23.05,10.32,23.05,23.05c0,12.72-10.32,23.05-23.05,23.05s-23.05-10.32-23.05-23.05 c0-8.21,3.56-14.68,10.02-18.76l-5.69-9.56h-15c1.52-3.28,2.47-6.09,2.28-7.7h15.32c0.63,0,1.22,0.17,1.74,0.47l1.61-3.35 l0.36-0.04l0,0c0.14-0.02,0.29-0.04,0.44-0.06c4.12-0.77,6.58-3.37,7.6-6.5c0.46-1.41,0.58-2.92,0.39-4.39 c-0.18-1.43-0.67-2.83-1.42-4.07c-0.33-0.54-0.71-1.05-1.13-1.53h3.83v0.02c0.49,0,0.99,0.11,1.47,0.33 c1.74,0.81,2.49,2.88,1.67,4.62L90.1,60.96l3.9,6.8C95.9,67.25,97.86,67.01,99.83,67.01L99.83,67.01z M34.14,30.59l21.03-15.38 c2.47-1.54,5.13-2.57,7.28-2.29c1.79,0.24,3.44,1.21,4.94,3.11c4.56,5.69,6.82,13.5,9.34,22.11l8.87-1.04 c5.07-0.23,6.4,7.22,1.19,8.2l-9.14,1.01c-4.08,0.46-7.06,1.59-9.28-3.01l-4.96-12.44l-14.27,9.92c-0.88,0.92-1.01,1.83-0.13,2.75 l10.71,7.79c1.41,0.95,2.6,1.62,2.89,3.51c0.44,2.84-11.58,21.71-13.68,25.35c-3.69,4.1-10.69,0.39-7.78-4.58l8.14-17.15 c-2.42-1.44-4.77-2.88-7.03-4.32C34.72,49.35,24.69,38.38,34.14,30.59L34.14,30.59z M81.6,0c5.49,0,9.94,4.45,9.94,9.94 c0,5.49-4.45,9.94-9.94,9.94c-5.49,0-9.94-4.45-9.94-9.94C71.66,4.45,76.11,0,81.6,0L81.6,0z M41.25,61.73h-4.77l-2.83,8.23 c1.03,0.49,2,1.04,2.91,1.64l-0.68,1.44c-1.09,2.03-1.45,4.03-1.24,5.88c-1.3-1.35-2.83-2.48-4.54-3.31l0,0l-5.76,13.02 c-0.77,1.76-2.83,2.56-4.59,1.79c-1.76-0.77-2.56-2.83-1.78-4.59l5.24-11.84c-0.05,0-0.11,0-0.16,0c-8.88,0-16.07,7.2-16.07,16.08 c0,8.87,7.2,16.07,16.07,16.07c8.88,0,16.07-7.2,16.07-16.07c0-1.56-0.22-3.06-0.63-4.49c0.15,0.11,0.3,0.22,0.46,0.33 c1.28,0.87,2.76,1.47,4.3,1.72c0.9,0.15,1.82,0.19,2.75,0.12c0.07,0.76,0.1,1.53,0.1,2.32c0,12.72-10.32,23.05-23.05,23.05 C10.32,113.11,0,102.78,0,90.06c0-12.73,10.32-23.05,23.05-23.05c1.07,0,2.12,0.07,3.16,0.21l4.09-11.11 c0.47-1.08,1.43-1.8,2.51-2.01c2.06,1.97,4.27,3.67,6.31,4.96c0.62,0.39,1.5,0.94,2.62,1.63L41.25,61.73L41.25,61.73z M22.13,38.32 h2.32c0.04,1.14,0.2,2.29,0.49,3.43c0.6,2.43,1.75,4.76,3.23,6.93h-6.03l0,0l0,0c-1.13,0-2.17-0.47-2.93-1.22 c-0.77-0.77-1.24-1.82-1.24-2.98l0,0l0,0v-1.94l0,0l0,0c0-1.17,0.47-2.23,1.24-2.98C19.95,38.79,20.99,38.33,22.13,38.32 L22.13,38.32L22.13,38.32L22.13,38.32L22.13,38.32z M98.1,74.07l5.59,8.61c1.05,1.61,0.59,3.76-1.02,4.81 c-1.61,1.05-3.76,0.59-4.81-1.02l-6.53-10.06c-4.55,2.84-7.58,7.89-7.58,13.65c0,8.87,7.2,16.07,16.08,16.07 c8.88,0,16.08-7.2,16.08-16.07c0-8.88-7.2-16.08-16.08-16.08C99.25,73.97,98.67,74.01,98.1,74.07L98.1,74.07L98.1,74.07z\"/>\n                            </g>\n                        </svg>", "Cycling", 1 },
                    { 11, "<svg height=\"100%\" viewBox=\"0 0 115.63 122.88\">\n                            <g>\n                                <path d=\"M27.64,37.98l1.85,4.47l0.04,0.1l-0.1,0.05l-7.02,3.81l-0.1,0.06l-0.06-0.1l-2.34-3.72c-0.49,0.18-1.03,0.36-1.64,0.57 c-0.9,0.31-1.96,0.67-3.13,1.1c-1.39,0.51-2.7,1.04-3.85,1.59c-0.68,0.32-1.31,0.66-1.89,1.02c1.21,2.78,2.58,5.39,4.12,7.85 c1.71,2.74,3.61,5.26,5.68,7.57c1.76,1.97,3.42,3.51,4.99,4.79c1.56,1.27,3.05,2.29,4.47,3.2c9.1,5.85,11.98,10.59,15.23,15.91 c2.01,3.3,4.17,6.84,8.19,11.03c3.63,3.78,7.43,6.63,11.45,8.6c3.99,1.96,8.23,3.05,12.77,3.35c6.78,0.44,15.63-0.46,24.13-1.32 c3.94-0.4,7.81-0.79,11.37-1.05l-0.01-0.05c-0.2-1.2-0.46-2.25-0.81-3.15c-0.32-0.82-0.71-1.49-1.2-2.02 c-2.22-2.39-6.01-2.25-9.89-2.11c-2.89,0.1-5.83,0.21-8.59-0.59c-5.25-1.53-9.15-5.51-11.97-10.6c-1.92,1.45-3.8,2.43-5.71,2.85 c-2.29,0.51-4.49,0.21-6.67-1.02c-1.3-0.74-1.75-2.39-1.02-3.69c0.74-1.3,2.39-1.75,3.69-1.02c0.94,0.53,1.87,0.66,2.83,0.45 c1.19-0.26,2.51-1.02,3.99-2.19c0.18-0.14,0.37-0.26,0.57-0.35c-0.31-0.8-0.6-1.61-0.88-2.43c-1.62,1.11-3.22,1.87-4.83,2.22 c-2.29,0.51-4.49,0.21-6.67-1.02c-1.3-0.74-1.75-2.39-1.02-3.69c0.74-1.3,2.39-1.75,3.69-1.02c0.94,0.53,1.87,0.66,2.83,0.45 c1.19-0.26,2.51-1.02,3.99-2.19c0.15-0.12,0.3-0.22,0.47-0.3c-0.29-1.27-0.54-2.54-0.76-3.79c-1.62,1.11-3.22,1.87-4.84,2.23 c-2.29,0.51-4.49,0.21-6.67-1.02c-1.3-0.74-1.75-2.39-1.02-3.69c0.74-1.3,2.39-1.75,3.69-1.02c0.94,0.53,1.87,0.66,2.83,0.45 c1.19-0.26,2.51-1.02,3.99-2.19c0.37-0.3,0.8-0.48,1.23-0.55c-0.13-1.35-0.22-2.66-0.28-3.9l-0.01-0.06l-0.08-0.94 c-0.24-2.88-0.4-4.85-1.45-5.86c-1.91-1.85-4.95-1.45-7.93-1.05c-1.32,0.18-2.64,0.35-3.96,0.37c-4.41,0.05-8.4-0.87-11.81-3.15 c-3.37-2.26-6.1-5.82-8.01-11.07c-1.6-3.3-3.18-4.94-4.88-5.24c-1.75-0.31-3.91,0.71-6.57,2.76l-0.01,0.01L27.64,37.98L27.64,37.98 z M25.92,34.86c3.37-2.59,6.34-3.88,9.12-3.47L57.2,0.73c0.57-0.79,1.66-0.97,2.45-0.4c0.79,0.57,0.97,1.66,0.4,2.45L38.36,32.8 c1.59,1.19,3.02,3.12,4.35,5.89c0.03,0.06,0.05,0.12,0.07,0.18c1.63,4.53,3.91,7.55,6.69,9.41c2.77,1.86,6.1,2.6,9.82,2.56 c1.09-0.01,2.31-0.17,3.54-0.34c2.12-0.28,4.26-0.57,6.28-0.24l38.13-43.69c0.64-0.73,1.75-0.8,2.48-0.17s0.8,1.75,0.17,2.48 l-37.3,42.74c0.37,0.26,0.73,0.56,1.09,0.9c2,1.94,2.21,4.44,2.51,8.09c0.02,0.21,0.04,0.43,0.08,0.93 c0.01,0.07,0.01,0.14,0.01,0.21c0.31,6.99,1.8,16.11,5.28,23.26c2.48,5.11,5.96,9.16,10.72,10.55c2.22,0.64,4.88,0.55,7.49,0.46 c4.7-0.17,9.28-0.33,12.59,3.23c0.81,0.87,1.43,1.92,1.91,3.14c0.44,1.13,0.76,2.43,1,3.87c0.08,0.47,0.14,0.95,0.18,1.43 c0.11,0.22,0.19,0.46,0.2,0.72c0.01,0.27-0.03,0.53-0.13,0.77c0.01,1.95-0.3,3.91-1.06,5.66c-1,2.32-2.73,4.25-5.44,5.31 c-2.2,0.86-6.5,1.51-11.17,1.96c-6.43,0.61-13.67,0.87-16.93,0.78c-11.25-0.3-19.51-2.25-26.41-6.05 c-6.91-3.81-12.38-9.43-18.07-17.1c-6.74-9.08-8.31-10.26-13.84-14.4c-1.27-0.95-2.73-2.05-4.36-3.3 c-4.68-3.62-9.45-8.05-12.91-12.71c-2.54-3.42-4.4-7-5.05-10.52c-0.68-3.71-0.04-7.32,2.48-10.59c0.91-1.17,2.06-2.29,3.47-3.35 c1.02-0.76,2.23-1.45,3.56-2.09c1.32-0.63,2.72-1.2,4.16-1.73c1.35-0.5,2.35-0.84,3.2-1.13c2.2-0.76,3.39-1.17,4.94-2.17 c0.46-0.3,0.99-0.67,1.61-1.13c0.67-0.5,1.38-1.05,2.16-1.68C25.87,34.89,25.89,34.87,25.92,34.86L25.92,34.86z M6.54,49.18 c-0.38,0.38-0.72,0.77-1.02,1.16c-1.85,2.39-2.31,5.05-1.81,7.8c0.54,2.94,2.17,6.03,4.41,9.06c3.25,4.38,7.77,8.57,12.24,12.02 c1.77,1.37,3.13,2.39,4.32,3.28c5.85,4.39,7.52,5.63,14.56,15.12c5.4,7.27,10.54,12.58,16.94,16.11c6.41,3.53,14.16,5.34,24.8,5.63 c3.16,0.08,10.21-0.16,16.51-0.77c4.38-0.42,8.34-1,10.22-1.74c1.73-0.68,2.84-1.92,3.49-3.42c0.4-0.94,0.64-1.97,0.73-3.05 c-3.51,0.25-7.31,0.64-11.18,1.03c-8.65,0.88-17.66,1.79-24.69,1.34c-4.99-0.32-9.66-1.54-14.09-3.71 c-4.4-2.16-8.53-5.24-12.44-9.32c-4.29-4.47-6.55-8.18-8.66-11.64c-3.02-4.95-5.71-9.36-14.12-14.78c-1.5-0.96-3.08-2.05-4.79-3.43 c-1.69-1.38-3.48-3.04-5.39-5.17c-2.23-2.48-4.25-5.16-6.05-8.05C9.06,54.31,7.73,51.82,6.54,49.18L6.54,49.18z\"/>\n                            </g>\n                        </svg>", "Walk", 1 },
                    { 12, "<svg height=\"100%\" viewBox=\"0 0 122.88 119.63\">\n                            <g>\n                                <path d=\"M44.27,115.28H78.5L60.7,77.5L44.27,115.28L44.27,115.28z M59.51,1.93c0-1.06,0.86-1.93,1.93-1.93 c1.06,0,1.93,0.86,1.93,1.93v18.08l3.75,6.94l9.72-16.83c0.53-0.92,1.7-1.24,2.62-0.71c0.92,0.53,1.24,1.7,0.71,2.62l-10.9,18.89 l45.78,84.86h5.92c1.06,0,1.93,0.86,1.93,1.93c0,1.06-0.86,1.93-1.93,1.93H1.93c-1.06,0-1.93-0.86-1.93-1.93 c0-1.06,0.86-1.93,1.93-1.93h5.92l45.71-84.99L42.72,12.03c-0.53-0.92-0.21-2.09,0.71-2.62s2.09-0.21,2.62,0.71l9.64,16.69 l3.83-7.12V1.93L59.51,1.93z\"/>\n                            </g>\n                        </svg>", "Camping", 1 },
                    { 13, "<svg height=\"100%\" viewBox=\"0 0 122.88 117.02\">\n                            <g>\n                                <path class=\"st0\" d=\"M30.17,112.3H44.4v4.71H30.17V112.3L30.17,112.3z M115.05,2.48c-0.3-1.02,0.28-2.1,1.3-2.4 c1.02-0.3,2.1,0.28,2.4,1.3c0.39,1.31,0.79,2.52,1.2,3.75c1.98,5.92,4.1,12.27,2.15,18.36c-2.02,6.29-6.47,9.62-11.2,13.15 c-0.64,0.48-1.29,0.96-1.85,1.39c-0.16,2.96-0.4,5.9-0.72,8.83c-0.8,7.37-2.09,14.65-3.93,21.83c2.35,2.2,4.17,4.76,5.45,7.5 c1.53,3.26,2.29,6.78,2.28,10.28c-0.01,3.51-0.81,7-2.41,10.17c-3.76,7.48-11.92,13.2-24.64,13.33h-0.02v0.01H32.61 c-0.04,0-0.08,0-0.12,0c-3.28,0.07-6.35-0.4-9.19-1.42c-2.9-1.05-5.55-2.66-7.94-4.87c-4.32-4-7.38-9-8.89-14.27 c-1.51-5.25-1.5-10.79,0.29-15.93c1.96-5.61,5.33-9.64,9.57-12.38l0.71-6.17c0.05-0.57,0.16-1.11,0.35-1.61 c0.22-0.58,0.55-1.09,0.99-1.52c0.27-0.27,1.53-0.74,3.25-1.26L16.1,31.98H7.96c-0.03,0-0.06,0-0.09,0 c-1.28,0.03-2.43-0.22-3.42-0.68c-1.43-0.66-2.52-1.74-3.27-3.03c-0.72-1.24-1.11-2.68-1.17-4.14c-0.06-1.45,0.21-2.92,0.82-4.24 c1.67-3.61,4.81-4.71,8.22-4.66c2.99,0.04,6.12,1.01,8.49,1.81c2.28,0.77,3.57,1.56,4.71,2.26c1.43,0.88,2.56,1.57,5.78,1.61 l9.01,0.1l0.04,0v0c2.66,0.07,4.4,1.13,5.3,2.57c0.48,0.77,0.71,1.63,0.69,2.52c-0.01,0.84-0.24,1.68-0.66,2.46 c-0.97,1.77-3,3.28-5.86,3.55c-0.06,0.01-0.12,0.01-0.18,0.01v0.01h-7.44l4.59,15.97c1.51-0.12,2.72-0.07,3.36,0.23 c2.13,1,2.91,4.62,3.44,7.12c0.11,0.53,0.21,1,0.28,1.26l0.01,0.04l44.39,4.46c2.54,0.1,4.91,0.45,7.1,0.99 c0.42-2.06,0.84-3.97,1.24-5.79c1.72-7.83,3.1-14.13,2.91-22.99l-8.42,7.37c-0.38,0.33-0.96,0.29-1.3-0.09l-3.57-4.08 c-0.33-0.38-0.29-0.96,0.09-1.3l26.28-23.01c0.38-0.33,0.96-0.29,1.29,0.09l3.57,4.08c0.33,0.38,0.29,0.96-0.09,1.3l-5.09,4.45 c0.12,0.25,0.2,0.52,0.2,0.82c0.09,3.36,0.09,6.7,0,10.02c3.97-2.97,7.63-5.9,9.18-10.74c1.56-4.89-0.35-10.62-2.13-15.96 C115.85,4.99,115.4,3.65,115.05,2.48L115.05,2.48z M99.75,30.35c0.18,0.28,0.29,0.61,0.31,0.96c0.48,10.41-1.02,17.25-2.92,25.89 c-0.43,1.94-0.87,3.97-1.32,6.15c1.92,0.75,3.68,1.66,5.26,2.72c1.57-6.46,2.69-12.99,3.41-19.61c0.32-2.96,0.56-5.93,0.72-8.92 c-0.08-0.35-0.06-0.71,0.05-1.05c0.18-3.69,0.23-7.4,0.18-11.13L99.75,30.35L99.75,30.35z M101.74,71.49l-0.06-0.01 c-0.61-0.16-1.08-0.6-1.3-1.15c-3.82-3.01-9.02-5.03-15.56-5.28c-0.05,0-0.1,0-0.15-0.01l-44.87-4.51 c-0.06,0.02-0.11,0.04-0.17,0.05c-0.53,0.14-1.06,0.04-1.49-0.22l-1.27-0.13c-6.23-0.3-12.3,0.64-17.19,3.36 c-4.13,2.3-7.43,5.9-9.27,11.16c-1.52,4.35-1.52,9.08-0.22,13.6c1.33,4.62,4,9,7.79,12.5c2,1.85,4.21,3.2,6.63,4.07 c2.38,0.86,4.98,1.25,7.79,1.2c0.07-0.01,0.14-0.01,0.21-0.01h52.46v0.01c11.05-0.12,18.05-4.93,21.21-11.22 c1.33-2.63,1.99-5.53,2-8.44c0.01-2.93-0.63-5.89-1.91-8.63C105.29,75.51,103.74,73.35,101.74,71.49L101.74,71.49z M20.48,58.93 c4.86-2.04,10.44-2.78,16.1-2.57l-0.02-0.12c-0.37-1.73-0.91-4.25-1.29-4.43c-0.37-0.17-2.75,0.19-5.48,0.71 c-1.61,0.31-3.26,0.65-4.69,0.97c-0.15,0.09-0.31,0.17-0.48,0.22c-0.26,0.08-0.52,0.1-0.77,0.07c-1.55,0.37-2.63,0.66-2.74,0.77 c-0.04,0.04-0.07,0.09-0.1,0.16c-0.05,0.13-0.08,0.3-0.1,0.51c0,0.04-0.01,0.08-0.01,0.12L20.48,58.93L20.48,58.93z M30.18,71.07 c3.15,0,5.99,1.28,8.05,3.34c2.06,2.06,3.34,4.91,3.34,8.06c0,3.15-1.28,5.99-3.34,8.05c-0.04,0.04-0.09,0.08-0.13,0.13l3.69,5.78 h7.9c1.07,0,1.93,0.87,1.93,1.93c0,1.07-0.87,1.93-1.93,1.93h-8.95v-0.01c-0.64,0-1.26-0.32-1.62-0.89l-4.19-6.57 c-1.44,0.66-3.05,1.03-4.75,1.03c-3.15,0-5.99-1.28-8.06-3.34c-2.06-2.06-3.34-4.91-3.34-8.05c0-3.15,1.28-5.99,3.34-8.06 C24.19,72.35,27.04,71.07,30.18,71.07L30.18,71.07z M35.5,77.15c-1.36-1.36-3.24-2.2-5.32-2.2c-2.08,0-3.96,0.84-5.32,2.2 c-1.36,1.36-2.2,3.24-2.2,5.32s0.84,3.96,2.2,5.32c1.36,1.36,3.24,2.2,5.32,2.2c0.93,0,1.81-0.17,2.63-0.47l-3.45-5.41 c-0.57-0.9-0.31-2.09,0.59-2.66c0.9-0.57,2.09-0.31,2.66,0.59l3.35,5.25c1.09-1.3,1.74-2.98,1.74-4.82 C37.7,80.39,36.86,78.51,35.5,77.15L35.5,77.15z M25.38,49.54c1.22-0.3,2.48-0.58,3.69-0.81c0.2-0.04,0.4-0.07,0.59-0.11 l-5.12-17.81c-0.07-0.2-0.1-0.41-0.1-0.62c0-1.07,0.87-1.93,1.93-1.93h9.96c1.37-0.16,2.29-0.8,2.69-1.54 c0.13-0.23,0.19-0.46,0.2-0.67c0-0.16-0.04-0.31-0.12-0.44c-0.26-0.41-0.94-0.72-2.13-0.75v0l-8.97-0.1 c-4.34-0.05-5.85-0.98-7.78-2.17c-0.98-0.6-2.09-1.29-3.91-1.9c-2.12-0.71-4.89-1.58-7.31-1.61c-2-0.03-3.8,0.54-4.67,2.42 c-0.35,0.77-0.51,1.63-0.48,2.48c0.03,0.84,0.26,1.66,0.66,2.35c0.37,0.63,0.89,1.15,1.55,1.46c0.48,0.22,1.04,0.34,1.69,0.33 c0.07-0.01,0.14-0.01,0.21-0.01h9.57v0c0.83,0,1.6,0.54,1.85,1.38L25.38,49.54L25.38,49.54z M77.22,112.3h14.23v4.71H77.22V112.3 L77.22,112.3z\"/>\n                            </g>\n                        </svg>", "Exercise Bike", 1 },
                    { 14, "<svg class=\"icon-weightlifting\" height=\"100%\" viewBox=\"0 0 122.88 116.01\">\n                            <defs><style>.cls-1{fill-rule:evenodd;}</style></defs>\n                            <path class=\"cls-1\" d=\"M122.88,29.45h-5.53V9.28h5.53V29.45ZM92.8,15.54h3.29a1.54,1.54,0,0,1,1.45,1h4.85v5.21H96.83c-6.43,11.76-13.25,24.1-20.9,34.51-1.37,1.86-1.52,1.49-2.06,3.5-1.1,4.19-1.05,7.67-1.09,12.11C74.58,76.88,80.9,92,80.9,95v21H71.16V95c0-1.72-6.06-8-7.64-11.41h0c-1.66.06-2.53.12-4.19.18l0,0c-1.7,3.45-7.57,9.55-7.57,11.24v21H42V95c0-3,6.29-18.11,8.11-23.12-.1-4.55,0-8.06-1-12-.54-2.13-.68-1.61-2.14-3.59-7.65-10.41-14.46-22.74-20.89-34.5H19.81V16.57h5.25a1.55,1.55,0,0,1,1.46-1h3.29a1.54,1.54,0,0,1,1.45,1H91.34a1.55,1.55,0,0,1,1.46-1ZM68.54,47.07h-.33c-4.38-1.25-8.67-1.51-12.87,0h-1c-7-7.48-13.4-16.51-20.73-25.29H89.27c-7.33,8.78-13.69,17.81-20.73,25.29Zm-7.1-22.95A9.88,9.88,0,1,1,51.57,34a9.87,9.87,0,0,1,9.87-9.88Zm54.83,14.24H105.88V0h10.39V38.36Zm-99.37,0H6.51V0H16.9V38.36ZM5.43,29.45H0V9.28H5.43V29.45Z\"/>\n                        </svg>", "Weight Lifting", 1 },
                    { 15, "<svg height=\"100%\" viewBox=\"0 0 122.88 96.96\">\n                            <path  d=\"M122.88,39.15c-2.84,32.48-23.77,52.06-58.93,57.81C65.11,56.71,91.95,45.24,122.88,39.15L122.88,39.15z M37.86,44.69C40.17,26.15,43.28,12.14,61.72,0c13.2,10.44,20.17,16.71,23.43,44.9c-9.72,2.33-18.39,12.82-23.54,28.85 C56.64,57.98,48.64,48.47,37.86,44.69L37.86,44.69z M91.73,42.62l-3.55-16.66c5.99-5.14,16.4-7.94,26.64-10.63 c0.24,6.13-0.68,12.87-1.34,19.51C105.53,36.17,98.36,38.87,91.73,42.62L91.73,42.62z M31.41,42.62l3.55-16.66 c-5.99-5.14-16.4-7.94-26.64-10.63c-0.24,6.13,0.68,12.87,1.34,19.51C17.61,36.17,24.79,38.87,31.41,42.62L31.41,42.62z M0,39.15 c2.84,32.48,23.77,52.06,58.93,57.81C57.77,56.71,30.93,45.24,0,39.15L0,39.15z\" />\n                        </svg>", "Yoga", 1 },
                    { 16, "<svg height=\"100%\" viewBox=\"0 0 512 428.64\">\n                            <g>\n                                <path d=\"M393.86 111.07l53.53 218.89c-20.6,11.07 -39.72,25.03 -57.93,36.84l-381.19 0c-4.52,0 -8.27,3.76 -8.27,8.27l0 30.9 512 0c0,-27.94 0,-55.88 0,-83.81 -0.47,-4.95 -1.55,-7.43 -3.59,-9.19 -3.94,-3.4 -25.23,2.05 -29.78,3.49 -2.57,0.81 -5.12,1.7 -7.65,2.66l-53.68 -209.08 80.68 -102.8 -22 -6.23c-11.56,-3.1 -16.32,0.98 -24.65,11.47 -24.99,31.49 -21.94,72.68 -82.25,74.7l-70.99 2.15c-9.33,0.3 -8.7,19.02 0,19l95.77 2.74zm-369.09 299.51l48.64 0 0 13.23c0,2.62 -2.21,4.83 -4.83,4.83l-39.81 0c-2.16,0 -4,-1.8 -4,-4l0 -14.06zm339.5 0l147.73 0 0 13.23c0,2.65 -2.18,4.83 -4.83,4.83l-138.9 0c-2.19,0 -4,-1.8 -4,-4l0 -14.06z\" />\n                            </g>\n                        </svg>", "Treadmill", 1 },
                    { 17, "<svg fill-rule=\"evenodd\" clip-rule=\"evenodd\" height=\"100%\" viewBox=\"0 0 512 472.36\">\n                            <g>\n                                <path d=\"M248.05 1.7c86.17,10.77 169.08,59.36 248.86,144.18 17.4,23.5 22.89,43.85 -0.58,49.61l-2.18 0c0.08,0.21 0.13,0.44 0.13,0.68l0 14.02c0,8.7 -7.12,15.82 -15.82,15.82l-400.59 0c-8.7,0 -15.83,-7.12 -15.83,-15.82l0 -14.02c0,-0.16 0.02,-0.32 0.07,-0.47 -13.39,-0.21 -24.69,-5.75 -33.85,-14.56 -46.45,-44.71 -32.94,-110.61 20.83,-142.28 57.45,-33.83 150.95,-43.16 198.96,-37.16zm10.03 387.69l41.84 0c2.71,0 4.92,2.21 4.92,4.92l0 57c0,2.71 -2.21,4.92 -4.92,4.92l-41.84 0c-2.7,0 -4.92,-2.21 -4.92,-4.92l0 -57c0,-2.71 2.22,-4.92 4.92,-4.92zm0 -16.13l4.62 0 0 -95.82c-4.47,-2.48 -9.69,-4.31 -18.1,-3.39 -24.96,2.78 -38.17,35.29 -69.28,45.01 -4.29,1.35 -8.7,2.27 -13.23,2.75 6.75,22.32 17.67,40.9 31.53,55.07 12.36,12.66 27.14,21.84 43.42,27.07l0 -9.64c0,-5.75 2.36,-10.99 6.17,-14.81l0.06 -0.06c3.84,-3.82 9.07,-6.18 14.81,-6.18zm36.87 0l4.97 0c5.72,0 10.98,2.36 14.81,6.18l0.06 0.06c3.82,3.84 6.18,9.07 6.18,14.81l0 57c0,5.72 -2.36,10.98 -6.18,14.81l-0.06 0.06c-3.85,3.83 -9.1,6.18 -14.81,6.18l-41.84 0c-5.74,0 -10.98,-2.37 -14.81,-6.18l-0.06 -0.06c-3.83,-3.84 -6.17,-9.1 -6.17,-14.81l0 -13.93c-25.13,-6.05 -47.9,-19.05 -66.47,-38.07 -20.12,-20.6 -35.2,-48.23 -42.79,-81.63 -33.97,-10.92 -52.71,-40.97 -47.29,-80.14l270.03 0c-20.44,24.54 -39.19,41.07 -55.57,46.39l0 89.33zm-115.85 -261.47c75.78,-21.62 167.81,2.28 224.67,62.77 -65.71,-37 -113.36,-46.68 -228.13,-39.8 -15.26,0.92 -25.29,-14.78 3.46,-22.97zm-27.46 -69.36c105.84,-22.16 229.17,17.2 303.3,103.42 -90.56,-60.42 -157.61,-81.67 -316.79,-69.87 -26.39,1.95 -31.6,-24.13 13.49,-33.55z\" />\n                            </g>\n                        </svg>", "Mountain Biking", 1 },
                    { 18, "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" height=\"100%\" viewBox=\"0 0 65.61 122.88\" style=\"0 0 65.61 122.88\" xml:space=\"preserve\">\n                            <g>\n                                <path d=\"M44.3,19.33l-4.91,3.27l0.72,6.17c0.18-0.4,0.38-0.77,0.61-1.13c1.06-1.65,2.65-2.83,4.72-3.58 c0.06-0.02,0.12-0.04,0.19-0.05l0,0l8.36-1.21c1.36-0.2,1.6-0.24,1.58-0.43c-0.03-0.2-0.08-0.46-0.15-0.77 c-0.06-0.27-0.12-0.57-0.19-0.92c-0.33,0.08-0.65,0.15-0.98,0.2c-0.68,0.11-1.34,0.15-2,0.12c-0.71-0.03-1.4-0.14-2.09-0.32 c-0.64-0.17-1.27-0.4-1.89-0.7L44.3,19.33L44.3,19.33z M16.92,87.34l-8.09,1.17l4.69,32.4l2.59-0.37l3.17-5.27 c0.03-0.05,0.05-0.09,0.09-0.13c1.27-1.58,2.03-2.93,2.14-3.99c0.08-0.82-0.34-1.48-1.39-1.95c-0.07-0.03-0.14-0.07-0.21-0.12 l-1.7-1.27l0,0c-0.2-0.15-0.34-0.38-0.37-0.65C17.15,100.14,16.95,93.91,16.92,87.34L16.92,87.34z M1.95,40.92l13.93-2.02 c0.01-0.17,0.07-0.33,0.18-0.48c1.7-2.31,3.59-4.4,5.68-6.27c2.09-1.86,4.39-3.5,6.91-4.91l0,0c0.14-0.08,0.3-0.12,0.47-0.12 c0.51,0.01,0.92,0.43,0.91,0.94l-0.23,10.27l1.93-0.28l0,0c0.03-0.01,0.05-0.01,0.08-0.01l7.39-1.07L37.53,22.7 c-0.02-0.04-0.03-0.09-0.04-0.13c-0.23-0.88-0.82-1.6-1.58-2.07c-0.49-0.31-1.04-0.51-1.61-0.61c-0.57-0.09-1.16-0.08-1.72,0.07 l0,0l0,0c-0.84,0.22-1.62,0.74-2.18,1.6c-0.03,0.05-0.06,0.1-0.1,0.14c-0.32,0.4-0.91,0.46-1.3,0.14c-1.5-1.22-2.66-2.63-3.5-4.23 c-0.76-1.47-1.25-3.09-1.47-4.85l-5.17-0.93c-1.88-0.04-3.44,0.35-4.67,1.19c-1.24,0.85-2.18,2.18-2.8,4l0,0 c-0.01,0.02-0.02,0.05-0.03,0.07l-3.74,8.5l0,0l-0.01,0.01c-0.7,1.53-1.5,2.94-2.3,4.34C3.33,33.44,1.42,36.79,1.95,40.92 L1.95,40.92z M44.12,17.43c2.41-1.76,5.56-3.99,5.87-3.7c2.69,2.5,5.99-2.16,3.94-4.74l4.11-2.9C57.73,5.69,57.3,5.32,56.73,5 c-0.67-0.37-1.53-0.68-2.64-0.89l0,0c-0.11-0.02-0.23-0.07-0.33-0.13l-2.53-1.64c-0.13-0.07-0.24-0.17-0.32-0.3 c-0.02,0.01-0.04,0.02-0.07,0.03c-0.09,0.04-0.17,0.08-0.26,0.14l0,0c-0.06,0.04-0.12,0.06-0.18,0.09 c-2.39,0.83-3.78,0.25-4.75-0.15c-0.39-0.16-0.56-0.23-1.06,1.4l-1.1,3.58l0,0c-0.01,0.03-0.02,0.06-0.03,0.09 c-0.17,0.39-0.43,0.74-0.79,1.04c-0.32,0.26-0.71,0.49-1.18,0.67l-7.19,3.61c-0.02,0.01-0.04,0.02-0.06,0.03 c-1.42,0.74-2.96,1.08-4.58,1.1c-1.18,0.01-2.41-0.15-3.67-0.45c0.22,1.27,0.61,2.45,1.18,3.53c0.58,1.11,1.34,2.12,2.3,3.02 c0.76-0.83,1.68-1.35,2.66-1.61l0,0c0.82-0.21,1.67-0.24,2.49-0.11c0.82,0.13,1.61,0.43,2.3,0.87c0.75,0.47,1.39,1.11,1.85,1.88 l4.83-3.22l0,0C43.73,17.48,43.92,17.43,44.12,17.43L44.12,17.43z M39.4,38.78l-6.48,0.94l5.23,28.96l12.12,1.54l-0.02-0.12 c-0.67-4.87-0.76-5.49-7.05-5.76c-0.47,0.01-0.88-0.34-0.93-0.82L39.4,38.78L39.4,38.78z M40.79,34.7l3.23,27.84 c7.1,0.35,7.23,1.3,8.07,7.32c0.05,0.37,0.1,0.76,0.19,1.31c0.07,0.51-0.28,0.98-0.79,1.05c-0.11,0.02-0.21,0.01-0.31-0.01 l-13.93-1.77c-0.42-0.05-0.74-0.38-0.8-0.78L31.1,40.03l-2.12,0.31c-0.51,0.07-0.98-0.28-1.05-0.79c-0.01-0.07-0.01-0.14-0.01-0.21 l0.22-9.64c-1.85,1.14-3.57,2.42-5.17,3.85c-1.8,1.6-3.44,3.37-4.92,5.31c6.5,6.16,4.9,11.7,2.95,18.43 c-1,3.46-2.09,7.25-2.12,11.59c-0.01,2.66-0.04,5.17-0.06,7.58c-0.1,10.88-0.18,19.64,0.82,30.1l1.29,0.96 c1.84,0.85,2.57,2.14,2.41,3.79c-0.14,1.43-1.03,3.1-2.5,4.93l-3.38,5.62c-0.15,0.25-0.4,0.4-0.66,0.44v0l-3.94,0.57 c-0.51,0.07-0.98-0.28-1.05-0.79L6.98,88.6L0.18,41.67l0-0.01c-0.83-4.97,1.3-8.71,3.53-12.62c0.78-1.36,1.56-2.74,2.22-4.18l0,0 l3.72-8.46c0.76-2.2,1.93-3.83,3.5-4.91c1.57-1.08,3.52-1.58,5.83-1.51c0.06,0,0.11,0.01,0.17,0.02l5.99,1.08 c0.04,0.01,0.07,0.02,0.11,0.03l0,0c1.54,0.48,3.01,0.74,4.39,0.73c1.33-0.02,2.58-0.29,3.74-0.9c0.03-0.01,0.06-0.03,0.08-0.04 l7.24-3.64c0.03-0.02,0.06-0.03,0.09-0.04l0,0c0.29-0.11,0.52-0.24,0.69-0.37c0.12-0.1,0.2-0.2,0.26-0.31L42.82,3 c1.1-3.56,1.86-3.25,3.54-2.56c0.69,0.28,1.68,0.69,3.35,0.14c0.14-0.08,0.27-0.15,0.39-0.2c0.03-0.01,0.06-0.02,0.09-0.03 c0.48-0.2,0.89-0.22,1.24-0.14c0.44,0.1,0.74,0.35,0.95,0.67l2.24,1.45c1.23,0.25,2.22,0.61,3.02,1.06c0.88,0.49,1.52,1.09,2,1.76 l0,0l0,0c1.18,1.68,1.95,3.51,2.19,5.43c0.21,1.66,0.01,3.38-0.65,5.12l3.22,5.12l0,0l0,0c0.97,1.56,1.37,3.26,1.15,4.88 c-0.21,1.59-1,3.09-2.39,4.3c-2.12,1.84-4.07,2.1-6.45,2.41c-0.29,0.04-0.59,0.08-1.05,0.14L40.79,34.7L40.79,34.7z\" />\n                            </g>\n                        </svg>", "Aerobics", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AppUserId",
                table: "Activities",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_IntensityId",
                table: "Activities",
                column: "IntensityId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TypeId",
                table: "Activities",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTypes_IconId",
                table: "ActivityTypes",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StateId",
                table: "AspNetUsers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FodmapAliases_FodmapId",
                table: "FodmapAliases",
                column: "FodmapId");

            migrationBuilder.CreateIndex(
                name: "IX_Fodmaps_CategoryId",
                table: "Fodmaps",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Fodmaps_ColorId",
                table: "Fodmaps",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Fodmaps_MaxUseUnitsId",
                table: "Fodmaps",
                column: "MaxUseUnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_FodmapId",
                table: "Food",
                column: "FodmapId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodAliases_FoodId",
                table: "FoodAliases",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Icons_IconGroupTypeId",
                table: "Icons",
                column: "IconGroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMaps_IngredientFoodId",
                table: "IngredientMaps",
                column: "IngredientFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMaps_ParentFoodId",
                table: "IngredientMaps",
                column: "ParentFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_FoodId",
                table: "MealItems",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_MealId",
                table: "MealItems",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_VolumeUnitsId",
                table: "MealItems",
                column: "VolumeUnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_AppUserId",
                table: "Meals",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_ColorId",
                table: "Meals",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealTypeId",
                table: "Meals",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionCategories_IconId",
                table: "ReactionCategories",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ActivityId",
                table: "Reactions",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_AppUserId",
                table: "Reactions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_FoodId",
                table: "Reactions",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_MealId",
                table: "Reactions",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_SeverityId",
                table: "Reactions",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_SourceTypeId",
                table: "Reactions",
                column: "SourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_TypeId",
                table: "Reactions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionTypes_CategoryId",
                table: "ReactionTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitTypeId",
                table: "Units",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSafeFoods_FoodId",
                table: "UserSafeFoods",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "FodmapAliases");

            migrationBuilder.DropTable(
                name: "FoodAliases");

            migrationBuilder.DropTable(
                name: "IngredientMaps");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "MealItems");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "UserSafeDays");

            migrationBuilder.DropTable(
                name: "UserSafeFoods");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "ReactionSeverities");

            migrationBuilder.DropTable(
                name: "ReactionSourceTypes");

            migrationBuilder.DropTable(
                name: "ReactionTypes");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "ActivityIntensities");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MealTypes");

            migrationBuilder.DropTable(
                name: "ReactionCategories");

            migrationBuilder.DropTable(
                name: "Fodmaps");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "FodmapCategories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "IconGroupTypes");

            migrationBuilder.DropTable(
                name: "UnitTypes");
        }
    }
}
