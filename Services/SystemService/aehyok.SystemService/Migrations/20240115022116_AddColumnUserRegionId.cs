using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnUserRegionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "区域Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_RegionId",
                table: "User",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Region_RegionId",
                table: "User",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Region_RegionId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RegionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "User");
        }
    }
}
