using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkFlowState_WorkFlowDefineId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowState_WorkFlowDefine_WorkFlowDefineId",
            //    table: "WorkFlowState");

            migrationBuilder.DropColumn(
                name: "WorkFlowId",
                table: "WorkFlowState");

            migrationBuilder.AlterColumn<long>(
                name: "WorkFlowDefineId",
                table: "WorkFlowState",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流Id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "WorkFlowState",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "WorkFlowDefine",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "WorkFlowAction",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "是否启用");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowState_WorkFlowDefine_WorkFlowDefineId",
            //    table: "WorkFlowState",
            //    column: "WorkFlowDefineId",
            //    principalTable: "WorkFlowDefine",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowState_WorkFlowDefine_WorkFlowDefineId",
            //    table: "WorkFlowState");

            migrationBuilder.AlterColumn<long>(
                name: "WorkFlowDefineId",
                table: "WorkFlowState",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "工作流Id");

            migrationBuilder.AlterColumn<int>(
                name: "IsEnable",
                table: "WorkFlowState",
                type: "int",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否启用");

            migrationBuilder.AddColumn<long>(
                name: "WorkFlowId",
                table: "WorkFlowState",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流Id");

            migrationBuilder.AlterColumn<int>(
                name: "IsEnable",
                table: "WorkFlowDefine",
                type: "int",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<int>(
                name: "IsEnable",
                table: "WorkFlowAction",
                type: "int",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否启用");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowState_WorkFlowDefine_WorkFlowDefineId",
            //    table: "WorkFlowState",
            //    column: "WorkFlowDefineId",
            //    principalTable: "WorkFlowDefine",
            //    principalColumn: "Id");
        }
    }
}
