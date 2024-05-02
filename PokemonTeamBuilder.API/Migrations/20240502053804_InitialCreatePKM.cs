using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonTeamBuilder.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePKM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PokemonTeamBuilder");

            migrationBuilder.CreateTable(
                name: "Item_PokeAPI",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sprite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_PokeAPI", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_PokeAPI",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pokemon___3214EC26604EAF0F", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbility",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__3214EC273C1B459E", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonMove",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__3214EC273D048BD9", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonType",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__3214EC27C57EA4A8", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__3214EC27D0BCCEE9", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonBaseStat",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseStat = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PKM_API_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonBaseStat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PokemonBaseStat_Pokemon_PokeAPI",
                        column: x => x.PKM_API_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_PokeAPI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonSprite",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    PKM_API_ID = table.Column<int>(type: "int", nullable: false),
                    FrontDefault = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FrontShiny = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FrontFemale = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FrontShinyFemale = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSprite", x => x.PKM_API_ID);
                    table.ForeignKey(
                        name: "FK_PokemonID_Sprite",
                        column: x => x.PKM_API_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_PokeAPI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PKMAPI_Ability_Junc",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    PKM_API_ID = table.Column<int>(type: "int", nullable: false),
                    AbilityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PokmeonToAbility", x => new { x.PKM_API_ID, x.AbilityID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PKMAPI_Ability_Junc_PokemonAbility",
                        column: x => x.AbilityID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "PokemonAbility",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PKMAPI_Ability_Junc_Pokemon_PokeAP",
                        column: x => x.PKM_API_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_PokeAPI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PKMAPI_Move_Junc",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    MoveID = table.Column<int>(type: "int", nullable: false),
                    PKM_API_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PokemonToMove", x => new { x.MoveID, x.PKM_API_ID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PKMAPI_Move_Junc_PokemonMove",
                        column: x => x.MoveID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "PokemonMove",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PKMAPI_Move_Junc_Pokemon_PokeAPI",
                        column: x => x.PKM_API_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_PokeAPI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PKMAPI_Type_Junc",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    PKM_API_ID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PokemonToType", x => new { x.PKM_API_ID, x.TypeID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PKMAPI_Type_Junc_PokemonType",
                        column: x => x.TypeID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "PokemonType",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PKMAPI_Type_Junc_Pokemon_PokeAPI",
                        column: x => x.PKM_API_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_PokeAPI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonTeam",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrainerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PokemonT__3214EC27AE892B21", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainerID_Team",
                        column: x => x.TrainerID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Trainer",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_TeamMember",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    PKM_API_ID = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ChosenAbilityID = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    IsShiny = table.Column<bool>(type: "bit", nullable: false),
                    TeraType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HeldItemID = table.Column<int>(type: "int", nullable: false),
                    PokemonTeamID = table.Column<int>(type: "int", nullable: false),
                    RosterOrder = table.Column<int>(type: "int", nullable: false),
                    Nature = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_TeamMember", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pokemon_TeamMember_Item_PokeAPI",
                        column: x => x.HeldItemID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Item_PokeAPI",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pokemon_TeamMember_PokemonAbility",
                        column: x => x.ChosenAbilityID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "PokemonAbility",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pokemon_TeamMember_PokemonTeam",
                        column: x => x.PokemonTeamID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "PokemonTeam",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pokemon_TeamMember_Pokemon_PokeAPI",
                        column: x => x.PKM_API_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_PokeAPI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonMoveSet",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    PKM_TM_ID = table.Column<int>(type: "int", nullable: false),
                    move_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    move_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    move_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    move_4 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMoveSet", x => x.PKM_TM_ID);
                    table.ForeignKey(
                        name: "FK_PokemonMoveSet_Pokemon_TeamMember",
                        column: x => x.PKM_TM_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_TeamMember",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonStat",
                schema: "PokemonTeamBuilder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Effort = table.Column<int>(type: "int", nullable: false),
                    Individual = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PKM_TM_ID = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PokemonStat_Pokemon_TeamMember",
                        column: x => x.PKM_TM_ID,
                        principalSchema: "PokemonTeamBuilder",
                        principalTable: "Pokemon_TeamMember",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PKMAPI_Ability_Junc_AbilityID",
                schema: "PokemonTeamBuilder",
                table: "PKMAPI_Ability_Junc",
                column: "AbilityID");

            migrationBuilder.CreateIndex(
                name: "IX_PKMAPI_Move_Junc_PKM_API_ID",
                schema: "PokemonTeamBuilder",
                table: "PKMAPI_Move_Junc",
                column: "PKM_API_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PKMAPI_Type_Junc_TypeID",
                schema: "PokemonTeamBuilder",
                table: "PKMAPI_Type_Junc",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TeamMember_ChosenAbilityID",
                schema: "PokemonTeamBuilder",
                table: "Pokemon_TeamMember",
                column: "ChosenAbilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TeamMember_HeldItemID",
                schema: "PokemonTeamBuilder",
                table: "Pokemon_TeamMember",
                column: "HeldItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TeamMember_PKM_API_ID",
                schema: "PokemonTeamBuilder",
                table: "Pokemon_TeamMember",
                column: "PKM_API_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TeamMember_PokemonTeamID",
                schema: "PokemonTeamBuilder",
                table: "Pokemon_TeamMember",
                column: "PokemonTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonBaseStat_PKM_API_ID",
                schema: "PokemonTeamBuilder",
                table: "PokemonBaseStat",
                column: "PKM_API_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStat_PKM_TM_ID",
                schema: "PokemonTeamBuilder",
                table: "PokemonStat",
                column: "PKM_TM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTeam_TrainerID",
                schema: "PokemonTeamBuilder",
                table: "PokemonTeam",
                column: "TrainerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PKMAPI_Ability_Junc",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PKMAPI_Move_Junc",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PKMAPI_Type_Junc",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonBaseStat",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonMoveSet",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonSprite",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonStat",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonMove",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonType",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "Pokemon_TeamMember",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "Item_PokeAPI",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonAbility",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "PokemonTeam",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "Pokemon_PokeAPI",
                schema: "PokemonTeamBuilder");

            migrationBuilder.DropTable(
                name: "Trainer",
                schema: "PokemonTeamBuilder");
        }
    }
}
