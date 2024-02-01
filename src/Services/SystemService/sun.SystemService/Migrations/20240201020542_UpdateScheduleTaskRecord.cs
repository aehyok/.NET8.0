using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTaskRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecuteTime",
                table: "ScheduleTaskRecord");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecuteEndTime",
                table: "ScheduleTaskRecord",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "执行结束时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecuteStartTime",
                table: "ScheduleTaskRecord",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "执行开始时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpressionTime",
                table: "ScheduleTaskRecord",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "表达式计算时间");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecuteEndTime",
                table: "ScheduleTaskRecord");

            migrationBuilder.DropColumn(
                name: "ExecuteStartTime",
                table: "ScheduleTaskRecord");

            migrationBuilder.DropColumn(
                name: "ExpressionTime",
                table: "ScheduleTaskRecord");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecuteTime",
                table: "ScheduleTaskRecord",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "执行时间");
        }
    }
}
