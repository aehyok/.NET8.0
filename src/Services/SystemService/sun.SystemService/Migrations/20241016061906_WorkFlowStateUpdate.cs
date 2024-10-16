using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class WorkFlowStateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowAction_AutoFormDefine_AutoFormDefineId",
                table: "WorkFlowAction");

            migrationBuilder.AlterColumn<long>(
                name: "AutoFormDefineId",
                table: "WorkFlowAction",
                type: "bigint",
                nullable: true,
                comment: "自定义form表单Id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "自定义form表单Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowAction_AutoFormDefine_AutoFormDefineId",
            //    table: "WorkFlowAction",
            //    column: "AutoFormDefineId",
            //    principalTable: "AutoFormDefine",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowAction_AutoFormDefine_AutoFormDefineId",
                table: "WorkFlowAction");

            migrationBuilder.AlterColumn<long>(
                name: "AutoFormDefineId",
                table: "WorkFlowAction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "自定义form表单Id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "自定义form表单Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowAction_AutoFormDefine_AutoFormDefineId",
            //    table: "WorkFlowAction",
            //    column: "AutoFormDefineId",
            //    principalTable: "AutoFormDefine",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
