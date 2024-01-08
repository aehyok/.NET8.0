using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTaskColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExecuteStatus",
                table: "ScheduleTask",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecuteTime",
                table: "ScheduleTask",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecuteStatus",
                table: "ScheduleTask");

            migrationBuilder.DropColumn(
                name: "ExecuteTime",
                table: "ScheduleTask");
        }
    }
}
