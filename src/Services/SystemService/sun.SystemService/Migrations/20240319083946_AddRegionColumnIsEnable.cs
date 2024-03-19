using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class AddRegionColumnIsEnable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "Region",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否启用");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "Region");
        }
    }
}
