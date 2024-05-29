using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowStateConfig_Region_RegionId",
            //    table: "WorkFlowStateConfig");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowStateLog_WorkFlowAction_WorkFlowActionId",
            //    table: "WorkFlowStateLog");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowStateConfig_RegionId",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "WorkFlowStateConfig");

            migrationBuilder.AlterColumn<long>(
                name: "WorkFlowActionId",
                table: "WorkFlowStateLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AddColumn<int>(
                name: "RegionLevel",
                table: "WorkFlowStateConfig",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "区域层级");

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "WorkFlowStateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "角色Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateConfig_RoleId",
                table: "WorkFlowStateConfig",
                column: "RoleId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowStateConfig_Role_RoleId",
            //    table: "WorkFlowStateConfig",
            //    column: "RoleId",
            //    principalTable: "Role",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowStateLog_WorkFlowAction_WorkFlowActionId",
            //    table: "WorkFlowStateLog",
            //    column: "WorkFlowActionId",
            //    principalTable: "WorkFlowAction",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowStateConfig_Role_RoleId",
            //    table: "WorkFlowStateConfig");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowStateLog_WorkFlowAction_WorkFlowActionId",
            //    table: "WorkFlowStateLog");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowStateConfig_RoleId",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropColumn(
                name: "RegionLevel",
                table: "WorkFlowStateConfig");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "WorkFlowStateConfig");

            migrationBuilder.AlterColumn<long>(
                name: "WorkFlowActionId",
                table: "WorkFlowStateLog",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "WorkFlowStateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流操作区域Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateConfig_RegionId",
                table: "WorkFlowStateConfig",
                column: "RegionId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowStateConfig_Region_RegionId",
            //    table: "WorkFlowStateConfig",
            //    column: "RegionId",
            //    principalTable: "Region",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowStateLog_WorkFlowAction_WorkFlowActionId",
            //    table: "WorkFlowStateLog",
            //    column: "WorkFlowActionId",
            //    principalTable: "WorkFlowAction",
            //    principalColumn: "Id");
        }
    }
}
