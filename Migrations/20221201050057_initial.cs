using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDECPiaBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rifas",
                columns: table => new
                {
                    idRifa = table.Column<int>(name: "id_Rifa", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreRifa = table.Column<string>(name: "nombre_Rifa", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    availablerifa = table.Column<bool>(name: "available_rifa", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rifas", x => x.idRifa);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    idplayers = table.Column<int>(name: "id_players", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emailplayers = table.Column<string>(name: "email_players", type: "nvarchar(max)", nullable: true),
                    idUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.idplayers);
                    table.ForeignKey(
                        name: "FK_Players_IdentityUser_userId",
                        column: x => x.userId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prizes",
                columns: table => new
                {
                    idPrize = table.Column<int>(name: "id_Prize", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idrifaprize = table.Column<int>(name: "id_rifa_prize", type: "int", nullable: false),
                    rifaidRifa = table.Column<int>(name: "rifaid_Rifa", type: "int", nullable: true),
                    nameprize = table.Column<string>(name: "name_prize", type: "nvarchar(max)", nullable: false),
                    availableprize = table.Column<bool>(name: "available_prize", type: "bit", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prizes", x => x.idPrize);
                    table.ForeignKey(
                        name: "FK_Prizes_Rifas_rifaid_Rifa",
                        column: x => x.rifaidRifa,
                        principalTable: "Rifas",
                        principalColumn: "id_Rifa");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    idGame = table.Column<int>(name: "id_Game", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPlayer = table.Column<string>(name: "id_Player", type: "nvarchar(max)", nullable: false),
                    playeridplayers = table.Column<int>(name: "playerid_players", type: "int", nullable: true),
                    idRifa = table.Column<int>(name: "id_Rifa", type: "int", nullable: false),
                    rifaidRifa = table.Column<int>(name: "rifaid_Rifa", type: "int", nullable: true),
                    NumeroLoteria = table.Column<int>(name: "Numero_Loteria", type: "int", nullable: false),
                    Winner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.idGame);
                    table.ForeignKey(
                        name: "FK_Games_Players_playerid_players",
                        column: x => x.playeridplayers,
                        principalTable: "Players",
                        principalColumn: "id_players");
                    table.ForeignKey(
                        name: "FK_Games_Rifas_rifaid_Rifa",
                        column: x => x.rifaidRifa,
                        principalTable: "Rifas",
                        principalColumn: "id_Rifa");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_playerid_players",
                table: "Games",
                column: "playerid_players");

            migrationBuilder.CreateIndex(
                name: "IX_Games_rifaid_Rifa",
                table: "Games",
                column: "rifaid_Rifa");

            migrationBuilder.CreateIndex(
                name: "IX_Players_userId",
                table: "Players",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Prizes_rifaid_Rifa",
                table: "Prizes",
                column: "rifaid_Rifa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Prizes");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Rifas");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
