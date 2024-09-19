using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class WorkFlowUpdateConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowActionConfig_Role_RoleId",
            //    table: "WorkFlowActionConfig");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowStateConfig_Role_RoleId",
            //    table: "WorkFlowStateConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowStateConfig_RoleId",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActionConfig_RoleId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "WorkFlowActionCirculateConfig");

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "WorkFlowActionConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "RegionLevel",
                table: "WorkFlowActionCirculateConfig",
                type: "int",
                nullable: false,
                comment: "区域等级",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "WorkFlowActionCirculateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "区域Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "WorkFlowActionCirculateConfig");

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "WorkFlowStateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "角色Id");

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "WorkFlowActionConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "角色Id");

            migrationBuilder.AlterColumn<int>(
                name: "RegionLevel",
                table: "WorkFlowActionCirculateConfig",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "区域等级");

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "WorkFlowActionCirculateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateConfig_RoleId",
                table: "WorkFlowStateConfig",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionConfig_RoleId",
                table: "WorkFlowActionConfig",
                column: "RoleId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowActionConfig_Role_RoleId",
            //    table: "WorkFlowActionConfig",
            //    column: "RoleId",
            //    principalTable: "Role",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowStateConfig_Role_RoleId",
            //    table: "WorkFlowStateConfig",
            //    column: "RoleId",
            //    principalTable: "Role",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
