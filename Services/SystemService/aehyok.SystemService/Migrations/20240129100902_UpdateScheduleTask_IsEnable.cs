using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTask_IsEnable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ScheduleTask");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "ScheduleTask",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否启用的状态");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "ScheduleTask");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ScheduleTask",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "状态");
        }
    }
}
