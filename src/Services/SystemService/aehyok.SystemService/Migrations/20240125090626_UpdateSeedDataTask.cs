using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "SeedDataTask",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SeedDataTask",
                type: "longtext",
                nullable: true,
                comment: "种子数据所属类的类名")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "SeedDataTask");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SeedDataTask",
                newName: "TaskName");
        }
    }
}
