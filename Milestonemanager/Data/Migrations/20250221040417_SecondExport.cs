using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milestonemanager.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondExport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTasks_Admins_AdminId1",
                table: "EventTasks");

            migrationBuilder.DropIndex(
                name: "IX_EventTasks_AdminId1",
                table: "EventTasks");

            migrationBuilder.DropColumn(
                name: "AdminId1",
                table: "EventTasks");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "EventTasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EventTasks_AdminId",
                table: "EventTasks",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTasks_Admins_AdminId",
                table: "EventTasks",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTasks_Admins_AdminId",
                table: "EventTasks");

            migrationBuilder.DropIndex(
                name: "IX_EventTasks_AdminId",
                table: "EventTasks");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "EventTasks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "AdminId1",
                table: "EventTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventTasks_AdminId1",
                table: "EventTasks",
                column: "AdminId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTasks_Admins_AdminId1",
                table: "EventTasks",
                column: "AdminId1",
                principalTable: "Admins",
                principalColumn: "AdminId");
        }
    }
}
