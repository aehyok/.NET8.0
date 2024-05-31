using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkFlowDefine_CollectFormMetaDataId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowActionConfig_WorkFlowStateConfig_WorkFlowStateConfig~",
            //    table: "WorkFlowActionConfig");

            //migrationBuilder.DropIndex(
            //    name: "IX_WorkFlowActionConfig_WorkFlowStateConfigId",
            //    table: "WorkFlowActionConfig");

            migrationBuilder.DropColumn(
                name: "WorkFlowStateConfigId",
                table: "WorkFlowActionConfig");

            migrationBuilder.AddColumn<long>(
                name: "CollectFormMetaDataId",
                table: "WorkFlowState",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "自定义form表单Id");

            migrationBuilder.AddColumn<int>(
                name: "RegionLevel",
                table: "WorkFlowActionConfig",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "区域层级");

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "WorkFlowActionConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "角色Id");

            migrationBuilder.AddColumn<long>(
                name: "WorkFlowActionId",
                table: "WorkFlowActionConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流程动作定义Id");

            migrationBuilder.AddColumn<long>(
                name: "CollectFormMetaDataId",
                table: "WorkFlowAction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "自定义form表单Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowState_CollectFormMetaDataId",
                table: "WorkFlowState",
                column: "CollectFormMetaDataId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionConfig_RoleId",
                table: "WorkFlowActionConfig",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionConfig_WorkFlowActionId",
                table: "WorkFlowActionConfig",
                column: "WorkFlowActionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowAction_CollectFormMetaDataId",
                table: "WorkFlowAction",
                column: "CollectFormMetaDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowAction_CollectFormMetaData_CollectFormMetaDataId",
                table: "WorkFlowAction",
                column: "CollectFormMetaDataId",
                principalTable: "CollectFormMetaData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActionConfig_Role_RoleId",
                table: "WorkFlowActionConfig",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActionConfig_WorkFlowAction_WorkFlowActionId",
                table: "WorkFlowActionConfig",
                column: "WorkFlowActionId",
                principalTable: "WorkFlowAction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowState_CollectFormMetaData_CollectFormMetaDataId",
                table: "WorkFlowState",
                column: "CollectFormMetaDataId",
                principalTable: "CollectFormMetaData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowAction_CollectFormMetaData_CollectFormMetaDataId",
                table: "WorkFlowAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActionConfig_Role_RoleId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActionConfig_WorkFlowAction_WorkFlowActionId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowState_CollectFormMetaData_CollectFormMetaDataId",
                table: "WorkFlowState");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowState_CollectFormMetaDataId",
                table: "WorkFlowState");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActionConfig_RoleId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActionConfig_WorkFlowActionId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowAction_CollectFormMetaDataId",
                table: "WorkFlowAction");

            migrationBuilder.DropColumn(
                name: "CollectFormMetaDataId",
                table: "WorkFlowState");

            migrationBuilder.DropColumn(
                name: "RegionLevel",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropColumn(
                name: "WorkFlowActionId",
                table: "WorkFlowActionConfig");

            migrationBuilder.DropColumn(
                name: "CollectFormMetaDataId",
                table: "WorkFlowAction");

            migrationBuilder.AddColumn<long>(
                name: "WorkFlowStateConfigId",
                table: "WorkFlowActionConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流状态配置Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkFlowActionConfig_WorkFlowStateConfigId",
            //    table: "WorkFlowActionConfig",
            //    column: "WorkFlowStateConfigId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowActionConfig_WorkFlowStateConfig_WorkFlowStateConfig~",
            //    table: "WorkFlowActionConfig",
            //    column: "WorkFlowStateConfigId",
            //    principalTable: "WorkFlowStateConfig",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
