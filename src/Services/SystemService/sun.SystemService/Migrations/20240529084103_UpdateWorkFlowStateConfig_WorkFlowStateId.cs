using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkFlowStateConfig_WorkFlowStateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowStateConfig_WorkFlowState_WorkFlowStateId",
            //    table: "WorkFlowStateConfig");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "WorkFlowStateConfig");

            migrationBuilder.AlterColumn<long>(
                name: "WorkFlowStateId",
                table: "WorkFlowStateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流状态Id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowStateConfig_WorkFlowState_WorkFlowStateId",
            //    table: "WorkFlowStateConfig",
            //    column: "WorkFlowStateId",
            //    principalTable: "WorkFlowState",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowStateConfig_WorkFlowState_WorkFlowStateId",
            //    table: "WorkFlowStateConfig");

            migrationBuilder.AlterColumn<long>(
                name: "WorkFlowStateId",
                table: "WorkFlowStateConfig",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "工作流状态Id");

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                table: "WorkFlowStateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流状态Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowStateConfig_WorkFlowState_WorkFlowStateId",
            //    table: "WorkFlowStateConfig",
            //    column: "WorkFlowStateId",
            //    principalTable: "WorkFlowState",
            //    principalColumn: "Id");
        }
    }
}
