using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CronTask");

            migrationBuilder.CreateTable(
                name: "ScheduleTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastWriteTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: ""),
                    TaskName = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTask", x => x.Id);
                },
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleTask");

            migrationBuilder.CreateTable(
                name: "CronTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: ""),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: ""),
                    LastWriteTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: ""),
                    TaskName = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CronTask", x => x.Id);
                },
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
