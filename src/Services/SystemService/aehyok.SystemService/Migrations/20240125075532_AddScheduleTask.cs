using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "File",
                type: "bigint",
                nullable: false,
                comment: "文件大小(字节) 1024字节=1KB",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "文件大小");

            migrationBuilder.CreateTable(
                name: "ScheduleTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: true, comment: "代码，默认为 Schedule 的类名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    CronExpression = table.Column<string>(type: "longtext", nullable: true, comment: "Cron 表达式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NextExecuteTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "下次执行时间"),
                    LastExecuteTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后一次执行时间"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTask", x => x.Id);
                },
                comment: "定时任务")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleTask");

            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "File",
                type: "bigint",
                nullable: false,
                comment: "文件大小",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "文件大小(字节) 1024字节=1KB");
        }
    }
}
