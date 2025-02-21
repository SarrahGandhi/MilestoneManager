using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milestonemanager.Data.Migrations
{
    /// <inheritdoc />
    public partial class FullExport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventLocation = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EventCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventGuests",
                columns: table => new
                {
                    GuestEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    IsRSVPAccepted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EventMen = table.Column<int>(type: "int", nullable: false),
                    EventWomen = table.Column<int>(type: "int", nullable: false),
                    EventKids = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGuests", x => x.GuestEventId);
                    table.ForeignKey(
                        name: "FK_EventGuests_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGuests_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaskName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskDescription = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EventTaskCategory = table.Column<int>(type: "int", nullable: false),
                    AdminId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_EventTasks_Admins_AdminId1",
                        column: x => x.AdminId1,
                        principalTable: "Admins",
                        principalColumn: "AdminId");
                    table.ForeignKey(
                        name: "FK_EventTasks_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EventGuests_EventId",
                table: "EventGuests",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGuests_GuestId",
                table: "EventGuests",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTasks_AdminId1",
                table: "EventTasks",
                column: "AdminId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventTasks_EventId",
                table: "EventTasks",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventGuests");

            migrationBuilder.DropTable(
                name: "EventTasks");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
