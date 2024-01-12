using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTaskColumnDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastWriteTime",
                table: "ScheduleTask",
                type: "datetime(6)",
                precision: 6,
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastWriteTime",
                table: "ScheduleTask",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldPrecision: 6,
                oldComment: "");
        }
    }
}
