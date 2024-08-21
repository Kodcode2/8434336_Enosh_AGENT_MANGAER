﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MosadRest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X_Waypoint = table.Column<int>(type: "int", nullable: false),
                    Y_Waypoint = table.Column<int>(type: "int", nullable: false),
                    Ststus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X_Waypoint = table.Column<int>(type: "int", nullable: false),
                    Y_Waypoint = table.Column<int>(type: "int", nullable: false),
                    Ststus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    TimeLeft = table.Column<float>(type: "real", nullable: false),
                    TotalExecutionTime = table.Column<float>(type: "real", nullable: false),
                    MissionStstus = table.Column<int>(type: "int", nullable: false),
                    AgentModelId = table.Column<int>(type: "int", nullable: true),
                    TargetModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Missions_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Missions_Agents_AgentModelId",
                        column: x => x.AgentModelId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Missions_Targets_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Targets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Missions_Targets_TargetModelId",
                        column: x => x.TargetModelId,
                        principalTable: "Targets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_AgentId",
                table: "Missions",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_AgentModelId",
                table: "Missions",
                column: "AgentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_TargetId",
                table: "Missions",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_TargetModelId",
                table: "Missions",
                column: "TargetModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Targets");
        }
    }
}
