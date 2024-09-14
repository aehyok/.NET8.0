using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class StateConfig_RegionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "WorkFlowStateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流操作区域Id");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "WorkFlowAction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "顺序");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateConfig_RegionId",
                table: "WorkFlowStateConfig",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowStateConfig_Region_RegionId",
                table: "WorkFlowStateConfig",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowStateConfig_Region_RegionId",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowStateConfig_RegionId",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "WorkFlowAction");
        }
    }
}
