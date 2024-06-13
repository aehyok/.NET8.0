using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkFlowRelationFormId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowAction_AutoFormDefine_CollectFormMetaDataId",
            //    table: "WorkFlowAction");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowActionLog_WorkFlowAction_WorkFlowActionId1",
            //    table: "WorkFlowActionLog");

            //migrationBuilder.DropIndex(
            //    name: "IX_WorkFlowActionLog_WorkFlowActionId1",
            //    table: "WorkFlowActionLog");

            //migrationBuilder.DropColumn(
            //    name: "WorkFlowActionId1",
            //    table: "WorkFlowActionLog");

            //migrationBuilder.RenameColumn(
            //    name: "CollectFormMetaDataId",
            //    table: "WorkFlowAction",
            //    newName: "AutoFormDefineId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_WorkFlowAction_CollectFormMetaDataId",
            //    table: "WorkFlowAction",
            //    newName: "IX_WorkFlowAction_AutoFormDefineId");

            migrationBuilder.AlterColumn<long>(
                name: "WorkFlowActionId",
                table: "WorkFlowActionLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "当前动作的Id",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "当前动作的Id")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_WorkFlowActionId",
                table: "WorkFlowActionLog",
                column: "WorkFlowActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowAction_AutoFormDefine_AutoFormDefineId",
                table: "WorkFlowAction",
                column: "AutoFormDefineId",
                principalTable: "AutoFormDefine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActionLog_WorkFlowAction_WorkFlowActionId",
                table: "WorkFlowActionLog",
                column: "WorkFlowActionId",
                principalTable: "WorkFlowAction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowAction_AutoFormDefine_AutoFormDefineId",
                table: "WorkFlowAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActionLog_WorkFlowAction_WorkFlowActionId",
                table: "WorkFlowActionLog");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActionLog_WorkFlowActionId",
                table: "WorkFlowActionLog");

            //migrationBuilder.RenameColumn(
            //    name: "AutoFormDefineId",
            //    table: "WorkFlowAction",
            //    newName: "CollectFormMetaDataId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_WorkFlowAction_AutoFormDefineId",
            //    table: "WorkFlowAction",
            //    newName: "IX_WorkFlowAction_CollectFormMetaDataId");

            migrationBuilder.AlterColumn<string>(
                name: "WorkFlowActionId",
                table: "WorkFlowActionLog",
                type: "longtext",
                nullable: true,
                comment: "当前动作的Id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "当前动作的Id")
                .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<long>(
            //    name: "WorkFlowActionId1",
            //    table: "WorkFlowActionLog",
            //    type: "bigint",
            //    nullable: true,
            //    comment: "");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkFlowActionLog_WorkFlowActionId1",
            //    table: "WorkFlowActionLog",
            //    column: "WorkFlowActionId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowAction_AutoFormDefine_CollectFormMetaDataId",
            //    table: "WorkFlowAction",
            //    column: "CollectFormMetaDataId",
            //    principalTable: "AutoFormDefine",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowActionLog_WorkFlowAction_WorkFlowActionId1",
            //    table: "WorkFlowActionLog",
            //    column: "WorkFlowActionId1",
            //    principalTable: "WorkFlowAction",
            //    principalColumn: "Id");
        }
    }
}
