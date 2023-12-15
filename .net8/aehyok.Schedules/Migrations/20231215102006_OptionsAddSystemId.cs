using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.Schedules.Migrations
{
    /// <inheritdoc />
    public partial class OptionsAddSystemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemId",
                table: "Role",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SystemId",
                table: "Options",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Options");
        }
    }
}
