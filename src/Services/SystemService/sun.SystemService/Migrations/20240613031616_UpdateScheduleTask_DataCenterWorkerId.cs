using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTask_DataCenterWorkerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowAction_FormDefine_CollectFormMetaDataId",
            //    table: "WorkFlowAction");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowState_FormDefine_FormDefineId",
            //    table: "WorkFlowState");

            migrationBuilder.DropTable(
                name: "FormDefine");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowState_FormDefineId",
                table: "WorkFlowState");

            migrationBuilder.DropColumn(
                name: "FormDefineId",
                table: "WorkFlowState");

            migrationBuilder.AddColumn<long>(
                name: "DatacenterId",
                table: "ScheduleTask",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "数据中心编号（用于雪花 Id 生成）");

            migrationBuilder.AddColumn<long>(
                name: "WorkerId",
                table: "ScheduleTask",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "主机编号（用于雪花 Id 生成）");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowState_AutoFormDefineId",
                table: "WorkFlowState",
                column: "AutoFormDefineId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowAction_AutoFormDefine_CollectFormMetaDataId",
                table: "WorkFlowAction",
                column: "CollectFormMetaDataId",
                principalTable: "AutoFormDefine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowState_AutoFormDefine_AutoFormDefineId",
                table: "WorkFlowState",
                column: "AutoFormDefineId",
                principalTable: "AutoFormDefine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowAction_AutoFormDefine_CollectFormMetaDataId",
                table: "WorkFlowAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowState_AutoFormDefine_AutoFormDefineId",
                table: "WorkFlowState");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowState_AutoFormDefineId",
                table: "WorkFlowState");

            migrationBuilder.DropColumn(
                name: "DatacenterId",
                table: "ScheduleTask");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "ScheduleTask");

            migrationBuilder.AddColumn<long>(
                name: "FormDefineId",
                table: "WorkFlowState",
                type: "bigint",
                nullable: true,
                comment: "");

            migrationBuilder.CreateTable(
                name: "FormDefine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BehindSQL = table.Column<string>(type: "longtext", nullable: true, comment: "后置执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BizType = table.Column<string>(type: "longtext", nullable: true, comment: "本模型对应的业务类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChildrenInputDefine = table.Column<string>(type: "longtext", nullable: true, comment: "子表单")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    GetDataSQL = table.Column<string>(type: "longtext", nullable: true, comment: "表单编辑时，初始化界面数据执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GetInitSQL = table.Column<string>(type: "longtext", nullable: true, comment: "表单新增时，初始化界面数据执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InputDefine = table.Column<string>(type: "longtext", nullable: true, comment: "表单结构定义")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "表单的名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TableDefine = table.Column<string>(type: "longtext", nullable: true, comment: "表定义")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    WriteTables = table.Column<string>(type: "longtext", nullable: true, comment: "写入数据的表")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefine", x => x.Id);
                },
                comment: "form表单元数据实体")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowState_FormDefineId",
                table: "WorkFlowState",
                column: "FormDefineId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowAction_FormDefine_CollectFormMetaDataId",
            //    table: "WorkFlowAction",
            //    column: "CollectFormMetaDataId",
            //    principalTable: "FormDefine",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowState_FormDefine_FormDefineId",
            //    table: "WorkFlowState",
            //    column: "FormDefineId",
            //    principalTable: "FormDefine",
            //    principalColumn: "Id");
        }
    }
}
