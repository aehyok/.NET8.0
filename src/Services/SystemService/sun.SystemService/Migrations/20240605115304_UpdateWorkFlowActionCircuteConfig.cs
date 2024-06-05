using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkFlowActionCircuteConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowAction_CollectFormMetaData_CollectFormMetaDataId",
            //    table: "WorkFlowAction");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowActioncirculateConfig_Region_RegionId",
            //    table: "WorkFlowActioncirculateConfig");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowActioncirculateConfig_WorkFlowAction_WorkFlowActionId",
            //    table: "WorkFlowActioncirculateConfig");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowActioncirculateConfig_WorkFlowStateConfig_WorkFlowSt~",
            //    table: "WorkFlowActioncirculateConfig");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowState_CollectFormMetaData_CollectFormMetaDataId",
            //    table: "WorkFlowState");

            migrationBuilder.DropTable(
                name: "AutoForm");

            migrationBuilder.DropTable(
                name: "AutoGuideLine");

            migrationBuilder.DropTable(
                name: "CollectFormMetaData");

            migrationBuilder.DropTable(
                name: "CollectFormMetaDataLine");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowState_CollectFormMetaDataId",
                table: "WorkFlowState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFlowActioncirculateConfig",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActioncirculateConfig_RegionId",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActioncirculateConfig_WorkFlowActionId",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActioncirculateConfig_WorkFlowStateConfigId",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropColumn(
                name: "TargetRegionId",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropColumn(
                name: "WorkFlowActionId",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropColumn(
                name: "WorkFlowStateConfigId",
                table: "WorkFlowActioncirculateConfig");

            migrationBuilder.RenameTable(
                name: "WorkFlowActioncirculateConfig",
                newName: "WorkFlowActionCirculateConfig");

            migrationBuilder.RenameColumn(
                name: "CollectFormMetaDataId",
                table: "WorkFlowState",
                newName: "AutoFormDefineId");

            migrationBuilder.AddColumn<long>(
                name: "FormDefineId",
                table: "WorkFlowState",
                type: "bigint",
                nullable: true,
                comment: "");

            migrationBuilder.AddColumn<int>(
                name: "RegionLevel",
                table: "WorkFlowActionCirculateConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "WorkFlowActionCirculateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkFlowActionConfigId",
                table: "WorkFlowActionCirculateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentIds",
                table: "AutoTask",
                type: "longtext",
                nullable: true,
                comment: "填报附件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "AutoTask",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "结束时间");

            migrationBuilder.AddColumn<bool>(
                name: "IsMore",
                table: "AutoTask",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否允许填报多次");

            migrationBuilder.AddColumn<bool>(
                name: "IsPushMessage",
                table: "AutoTask",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否推送消息");

            migrationBuilder.AddColumn<long>(
                name: "PublishRegionId",
                table: "AutoTask",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "发布区域Id(单个)");

            migrationBuilder.AddColumn<string>(
                name: "PublishRegionIds",
                table: "AutoTask",
                type: "longtext",
                nullable: true,
                comment: "区域发布范围（多个）")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "AutoTask",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "当前任务发布所属区域");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateTime",
                table: "AutoTask",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "开始时间，精确到秒");

            migrationBuilder.AddColumn<long>(
                name: "AutoTaskId",
                table: "AutoRecord",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "自动化任务Id");

            migrationBuilder.AddColumn<string>(
                name: "FormBusinessId",
                table: "AutoRecord",
                type: "longtext",
                nullable: true,
                comment: "关联的表单业务表字段Id")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "AutoRecord",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "数据记录填报 区域Id");

            migrationBuilder.AddColumn<int>(
                name: "WriteStatus",
                table: "AutoRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "填写状态");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFlowActionCirculateConfig",
                table: "WorkFlowActionCirculateConfig",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AutoFormDefine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "表单的名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InputDefine = table.Column<string>(type: "longtext", nullable: true, comment: "表单结构定义")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChildrenInputDefine = table.Column<string>(type: "longtext", nullable: true, comment: "子表单")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GetInitSQL = table.Column<string>(type: "longtext", nullable: true, comment: "表单新增时，初始化界面数据执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GetDataSQL = table.Column<string>(type: "longtext", nullable: true, comment: "表单编辑时，初始化界面数据执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BehindSQL = table.Column<string>(type: "longtext", nullable: true, comment: "后置执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WriteTables = table.Column<string>(type: "longtext", nullable: true, comment: "写入数据的表")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TableDefine = table.Column<string>(type: "longtext", nullable: true, comment: "表定义")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    BizType = table.Column<string>(type: "longtext", nullable: true, comment: "本模型对应的业务类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoFormDefine", x => x.Id);
                },
                comment: "自定义表单定义")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoGuideLineDefine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "指标名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbzt = table.Column<string>(type: "longtext", nullable: true, comment: "指标主题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Algorithm = table.Column<string>(type: "longtext", nullable: true, comment: "指标算法")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Metadata = table.Column<string>(type: "longtext", nullable: true, comment: "指标元数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: "父级Id"),
                    SelectAlgorithm = table.Column<string>(type: "longtext", nullable: true, comment: "指标查询算法 指标查询的SELECT语句")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    jsmx_zbmeta = table.Column<string>(type: "longtext", nullable: true, comment: "明细_指标元数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, comment: "显示顺序"),
                    Descrption = table.Column<string>(type: "longtext", nullable: true, comment: "指标说明")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Md5 = table.Column<string>(type: "longtext", nullable: true, comment: "MD5值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoGuideLineDefine", x => x.Id);
                },
                comment: "form表单自动生成sql的指标定义表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormDefine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "表单的名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InputDefine = table.Column<string>(type: "longtext", nullable: true, comment: "表单结构定义")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChildrenInputDefine = table.Column<string>(type: "longtext", nullable: true, comment: "子表单")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GetInitSQL = table.Column<string>(type: "longtext", nullable: true, comment: "表单新增时，初始化界面数据执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GetDataSQL = table.Column<string>(type: "longtext", nullable: true, comment: "表单编辑时，初始化界面数据执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BehindSQL = table.Column<string>(type: "longtext", nullable: true, comment: "后置执行的SQL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WriteTables = table.Column<string>(type: "longtext", nullable: true, comment: "写入数据的表")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TableDefine = table.Column<string>(type: "longtext", nullable: true, comment: "表定义")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    BizType = table.Column<string>(type: "longtext", nullable: true, comment: "本模型对应的业务类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
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

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionCirculateConfig_WorkFlowActionConfigId",
                table: "WorkFlowActionCirculateConfig",
                column: "WorkFlowActionConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRecord_AutoTaskId",
                table: "AutoRecord",
                column: "AutoTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRecord_RegionId",
                table: "AutoRecord",
                column: "RegionId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoRecord_AutoTask_AutoTaskId",
            //    table: "AutoRecord",
            //    column: "AutoTaskId",
            //    principalTable: "AutoTask",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoRecord_Region_RegionId",
            //    table: "AutoRecord",
            //    column: "RegionId",
            //    principalTable: "Region",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowAction_FormDefine_CollectFormMetaDataId",
            //    table: "WorkFlowAction",
            //    column: "CollectFormMetaDataId",
            //    principalTable: "FormDefine",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowActionCirculateConfig_WorkFlowActionConfig_WorkFlowA~",
            //    table: "WorkFlowActionCirculateConfig",
            //    column: "WorkFlowActionConfigId",
            //    principalTable: "WorkFlowActionConfig",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowState_FormDefine_FormDefineId",
            //    table: "WorkFlowState",
            //    column: "FormDefineId",
            //    principalTable: "FormDefine",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoRecord_AutoTask_AutoTaskId",
            //    table: "AutoRecord");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoRecord_Region_RegionId",
            //    table: "AutoRecord");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowAction_FormDefine_CollectFormMetaDataId",
            //    table: "WorkFlowAction");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowActionCirculateConfig_WorkFlowActionConfig_WorkFlowA~",
            //    table: "WorkFlowActionCirculateConfig");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkFlowState_FormDefine_FormDefineId",
            //    table: "WorkFlowState");

            migrationBuilder.DropTable(
                name: "AutoFormDefine");

            migrationBuilder.DropTable(
                name: "AutoGuideLineDefine");

            migrationBuilder.DropTable(
                name: "FormDefine");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowState_FormDefineId",
                table: "WorkFlowState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFlowActionCirculateConfig",
                table: "WorkFlowActionCirculateConfig");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActionCirculateConfig_WorkFlowActionConfigId",
                table: "WorkFlowActionCirculateConfig");

            migrationBuilder.DropIndex(
                name: "IX_AutoRecord_AutoTaskId",
                table: "AutoRecord");

            migrationBuilder.DropIndex(
                name: "IX_AutoRecord_RegionId",
                table: "AutoRecord");

            migrationBuilder.DropColumn(
                name: "FormDefineId",
                table: "WorkFlowState");

            migrationBuilder.DropColumn(
                name: "RegionLevel",
                table: "WorkFlowActionCirculateConfig");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "WorkFlowActionCirculateConfig");

            migrationBuilder.DropColumn(
                name: "WorkFlowActionConfigId",
                table: "WorkFlowActionCirculateConfig");

            migrationBuilder.DropColumn(
                name: "AttachmentIds",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "IsMore",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "IsPushMessage",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "PublishRegionId",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "PublishRegionIds",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "StartDateTime",
                table: "AutoTask");

            migrationBuilder.DropColumn(
                name: "AutoTaskId",
                table: "AutoRecord");

            migrationBuilder.DropColumn(
                name: "FormBusinessId",
                table: "AutoRecord");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AutoRecord");

            migrationBuilder.DropColumn(
                name: "WriteStatus",
                table: "AutoRecord");

            migrationBuilder.RenameTable(
                name: "WorkFlowActionCirculateConfig",
                newName: "WorkFlowActioncirculateConfig");

            migrationBuilder.RenameColumn(
                name: "AutoFormDefineId",
                table: "WorkFlowState",
                newName: "CollectFormMetaDataId");

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "WorkFlowActioncirculateConfig",
                type: "bigint",
                nullable: true,
                comment: "");

            migrationBuilder.AddColumn<long>(
                name: "TargetRegionId",
                table: "WorkFlowActioncirculateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "目标所属区域Id");

            migrationBuilder.AddColumn<long>(
                name: "WorkFlowActionId",
                table: "WorkFlowActioncirculateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "所在流程的动作Id");

            migrationBuilder.AddColumn<long>(
                name: "WorkFlowStateConfigId",
                table: "WorkFlowActioncirculateConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "工作流 状态配置Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFlowActioncirculateConfig",
                table: "WorkFlowActioncirculateConfig",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AutoForm",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoForm", x => x.Id);
                },
                comment: "自定义表单表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoGuideLine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoGuideLine", x => x.Id);
                },
                comment: "自动化指标表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CollectFormMetaData",
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
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "表单的名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "1正常，2停用"),
                    TableDefine = table.Column<string>(type: "longtext", nullable: true, comment: "表定义")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    WriteTables = table.Column<string>(type: "longtext", nullable: true, comment: "写入数据的表")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectFormMetaData", x => x.Id);
                },
                comment: "form表单元数据实体")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CollectFormMetaDataLine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    fid = table.Column<long>(type: "bigint", nullable: false, comment: "父级Id"),
                    jsmx_zbmeta = table.Column<string>(type: "longtext", nullable: true, comment: "明细_指标元数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    md5 = table.Column<string>(type: "longtext", nullable: true, comment: "MD5值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    xsxh = table.Column<string>(type: "longtext", nullable: true, comment: "显示顺序")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbcxsf = table.Column<string>(type: "longtext", nullable: true, comment: "指标查询算法 指标查询的SELECT语句")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbmc = table.Column<string>(type: "longtext", nullable: true, comment: "指标名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbmeta = table.Column<string>(type: "longtext", nullable: true, comment: "指标元数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbsf = table.Column<string>(type: "longtext", nullable: true, comment: "指标算法")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbsm = table.Column<string>(type: "longtext", nullable: true, comment: "指标说明")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbzt = table.Column<string>(type: "longtext", nullable: true, comment: "指标主题")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectFormMetaDataLine", x => x.Id);
                },
                comment: "form表单自动生成sql的指标定义表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowState_CollectFormMetaDataId",
                table: "WorkFlowState",
                column: "CollectFormMetaDataId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActioncirculateConfig_RegionId",
                table: "WorkFlowActioncirculateConfig",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActioncirculateConfig_WorkFlowActionId",
                table: "WorkFlowActioncirculateConfig",
                column: "WorkFlowActionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActioncirculateConfig_WorkFlowStateConfigId",
                table: "WorkFlowActioncirculateConfig",
                column: "WorkFlowStateConfigId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowAction_CollectFormMetaData_CollectFormMetaDataId",
            //    table: "WorkFlowAction",
            //    column: "CollectFormMetaDataId",
            //    principalTable: "CollectFormMetaData",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowActioncirculateConfig_Region_RegionId",
            //    table: "WorkFlowActioncirculateConfig",
            //    column: "RegionId",
            //    principalTable: "Region",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowActioncirculateConfig_WorkFlowAction_WorkFlowActionId",
            //    table: "WorkFlowActioncirculateConfig",
            //    column: "WorkFlowActionId",
            //    principalTable: "WorkFlowAction",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowActioncirculateConfig_WorkFlowStateConfig_WorkFlowSt~",
            //    table: "WorkFlowActioncirculateConfig",
            //    column: "WorkFlowStateConfigId",
            //    principalTable: "WorkFlowStateConfig",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkFlowState_CollectFormMetaData_CollectFormMetaDataId",
            //    table: "WorkFlowState",
            //    column: "CollectFormMetaDataId",
            //    principalTable: "CollectFormMetaData",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
