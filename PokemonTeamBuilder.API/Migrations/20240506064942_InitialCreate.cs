using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PokemonTeamBuilder.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemPokeApis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sprite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPokeApis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMoves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonPokeApis",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonPokeApis", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilityPokemonPokeApi",
                columns: table => new
                {
                    AbilitiesId = table.Column<int>(type: "int", nullable: false),
                    PkmApisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilityPokemonPokeApi", x => new { x.AbilitiesId, x.PkmApisId });
                    table.ForeignKey(
                        name: "FK_PokemonAbilityPokemonPokeApi_PokemonAbilities_AbilitiesId",
                        column: x => x.AbilitiesId,
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAbilityPokemonPokeApi_PokemonPokeApis_PkmApisId",
                        column: x => x.PkmApisId,
                        principalTable: "PokemonPokeApis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonBaseStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseStat = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PkmApiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonBaseStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonBaseStats_PokemonPokeApis_PkmApiId",
                        column: x => x.PkmApiId,
                        principalTable: "PokemonPokeApis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonMovePokemonPokeApi",
                columns: table => new
                {
                    MovesId = table.Column<int>(type: "int", nullable: false),
                    PkmApisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMovePokemonPokeApi", x => new { x.MovesId, x.PkmApisId });
                    table.ForeignKey(
                        name: "FK_PokemonMovePokemonPokeApi_PokemonMoves_MovesId",
                        column: x => x.MovesId,
                        principalTable: "PokemonMoves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonMovePokemonPokeApi_PokemonPokeApis_PkmApisId",
                        column: x => x.PkmApisId,
                        principalTable: "PokemonPokeApis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSprites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrontDefault = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontShiny = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontFemale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontShinyFemale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PkmApiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSprites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonSprites_PokemonPokeApis_PkmApiId",
                        column: x => x.PkmApiId,
                        principalTable: "PokemonPokeApis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonPokeApiPokemonType",
                columns: table => new
                {
                    PkmApisId = table.Column<int>(type: "int", nullable: false),
                    TypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonPokeApiPokemonType", x => new { x.PkmApisId, x.TypesId });
                    table.ForeignKey(
                        name: "FK_PokemonPokeApiPokemonType_PokemonPokeApis_PkmApisId",
                        column: x => x.PkmApisId,
                        principalTable: "PokemonPokeApis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonPokeApiPokemonType_PokemonTypes_TypesId",
                        column: x => x.TypesId,
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTeams_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PkmApiId = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ChosenAbility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    IsShiny = table.Column<bool>(type: "bit", nullable: false),
                    TeraType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeldItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonTeamId = table.Column<int>(type: "int", nullable: false),
                    RosterOrder = table.Column<int>(type: "int", nullable: false),
                    Nature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemPokeApiId = table.Column<int>(type: "int", nullable: true),
                    PokemonAbilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTeamMembers_ItemPokeApis_ItemPokeApiId",
                        column: x => x.ItemPokeApiId,
                        principalTable: "ItemPokeApis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonTeamMembers_PokemonAbilities_PokemonAbilityId",
                        column: x => x.PokemonAbilityId,
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonTeamMembers_PokemonPokeApis_PkmApiId",
                        column: x => x.PkmApiId,
                        principalTable: "PokemonPokeApis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTeamMembers_PokemonTeams_PokemonTeamId",
                        column: x => x.PokemonTeamId,
                        principalTable: "PokemonTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonMoveSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Move1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Move2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Move3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Move4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PkmTmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMoveSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonMoveSets_PokemonTeamMembers_PkmTmId",
                        column: x => x.PkmTmId,
                        principalTable: "PokemonTeamMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Effort = table.Column<int>(type: "int", nullable: false),
                    Individual = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PkmTmId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStats_PokemonTeamMembers_PkmTmId",
                        column: x => x.PkmTmId,
                        principalTable: "PokemonTeamMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "TestTrainer" },
                    { 42, "TestTrainer42" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilityPokemonPokeApi_PkmApisId",
                table: "PokemonAbilityPokemonPokeApi",
                column: "PkmApisId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonBaseStats_PkmApiId",
                table: "PokemonBaseStats",
                column: "PkmApiId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMovePokemonPokeApi_PkmApisId",
                table: "PokemonMovePokemonPokeApi",
                column: "PkmApisId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoveSets_PkmTmId",
                table: "PokemonMoveSets",
                column: "PkmTmId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonPokeApiPokemonType_TypesId",
                table: "PokemonPokeApiPokemonType",
                column: "TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSprites_PkmApiId",
                table: "PokemonSprites",
                column: "PkmApiId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStats_PkmTmId",
                table: "PokemonStats",
                column: "PkmTmId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTeamMembers_ItemPokeApiId",
                table: "PokemonTeamMembers",
                column: "ItemPokeApiId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTeamMembers_PkmApiId",
                table: "PokemonTeamMembers",
                column: "PkmApiId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTeamMembers_PokemonAbilityId",
                table: "PokemonTeamMembers",
                column: "PokemonAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTeamMembers_PokemonTeamId",
                table: "PokemonTeamMembers",
                column: "PokemonTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTeams_TrainerId",
                table: "PokemonTeams",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbilityPokemonPokeApi");

            migrationBuilder.DropTable(
                name: "PokemonBaseStats");

            migrationBuilder.DropTable(
                name: "PokemonMovePokemonPokeApi");

            migrationBuilder.DropTable(
                name: "PokemonMoveSets");

            migrationBuilder.DropTable(
                name: "PokemonPokeApiPokemonType");

            migrationBuilder.DropTable(
                name: "PokemonSprites");

            migrationBuilder.DropTable(
                name: "PokemonStats");

            migrationBuilder.DropTable(
                name: "PokemonMoves");

            migrationBuilder.DropTable(
                name: "PokemonTypes");

            migrationBuilder.DropTable(
                name: "PokemonTeamMembers");

            migrationBuilder.DropTable(
                name: "ItemPokeApis");

            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "PokemonPokeApis");

            migrationBuilder.DropTable(
                name: "PokemonTeams");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
