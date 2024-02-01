using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchudleTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Options_TenantId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Options");

            migrationBuilder.CreateTable(
                name: "ScheduleTaskRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScheduleTaskId = table.Column<long>(type: "bigint", nullable: false, comment: "任务 Id"),
                    IsSuccess = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否成功，没报错即成功"),
                    ErrorMessage = table.Column<string>(type: "longtext", nullable: true, comment: "如果执行失败，错误消息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExecuteTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "执行时间"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTaskRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleTaskRecord_ScheduleTask_ScheduleTaskId",
                        column: x => x.ScheduleTaskId,
                        principalTable: "ScheduleTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTaskRecord_ScheduleTaskId",
                table: "ScheduleTaskRecord",
                column: "ScheduleTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleTaskRecord");

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Options",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "租户Id");

            migrationBuilder.CreateIndex(
                name: "IX_Options_TenantId",
                table: "Options",
                column: "TenantId");
        }
    }
}
