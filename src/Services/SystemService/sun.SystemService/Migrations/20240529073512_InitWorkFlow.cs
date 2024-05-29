using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class InitWorkFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkFlowAssignLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false, comment: "所属业务Id"),
                    WorkFlowStateId = table.Column<long>(type: "bigint", nullable: false, comment: "工作流状态Id"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "指派开始时间"),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "指派结束时间"),
                    AssignUserId = table.Column<long>(type: "bigint", nullable: false, comment: "指派到的用户Id"),
                    AssignRegionId = table.Column<long>(type: "bigint", nullable: false, comment: " 指派到的区域Id "),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "操作用户Id"),
                    CreateRegionId = table.Column<long>(type: "bigint", nullable: false, comment: "操作区域Id"),
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
                    table.PrimaryKey("PK_WorkFlowAssignLog", x => x.Id);
                },
                comment: "工作流 指派日志")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowDefine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FlowName = table.Column<string>(type: "longtext", nullable: true, comment: "流程名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: true, comment: "流程Code唯一编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowType = table.Column<string>(type: "longtext", nullable: true, comment: "流程分类")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descriptionn = table.Column<string>(type: "longtext", nullable: true, comment: "流程描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, comment: "顺序"),
                    IsEnable = table.Column<int>(type: "int", nullable: false, comment: "是否启用"),
                    JsonDefine = table.Column<string>(type: "longtext", nullable: true, comment: "JSON定义包")
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
                    table.PrimaryKey("PK_WorkFlowDefine", x => x.Id);
                },
                comment: "工作流定义")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowDocument",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkFlowDefineId = table.Column<long>(type: "bigint", nullable: false, comment: "工作流定义Id"),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "文书名称")
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
                    table.PrimaryKey("PK_WorkFlowDocument", x => x.Id);
                },
                comment: "工作流中配置的生成文档（docx excel pdf等）")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowFormDefaultConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_WorkFlowFormDefaultConfig", x => x.Id);
                },
                comment: "Form表单默认值配置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowState",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JsonDefineId = table.Column<string>(type: "longtext", nullable: true, comment: "所在JSON元数据中的唯一ID（GUID）")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowId = table.Column<long>(type: "bigint", nullable: false, comment: "工作流Id"),
                    WorkFlowDefineId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    StateName = table.Column<string>(type: "longtext", nullable: true, comment: "状态名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StateCode = table.Column<string>(type: "longtext", nullable: true, comment: "状态Code唯一编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true, comment: "状态描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StateType = table.Column<int>(type: "int", nullable: false, comment: "状态类型"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, comment: "顺序"),
                    IsEnable = table.Column<int>(type: "int", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_WorkFlowState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowState_WorkFlowDefine_WorkFlowDefineId",
                        column: x => x.WorkFlowDefineId,
                        principalTable: "WorkFlowDefine",
                        principalColumn: "Id");
                },
                comment: "工作流下的状态表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowAction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JsonDefineId = table.Column<string>(type: "longtext", nullable: true, comment: "所在JSON元数据中的唯一ID（GUID）")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowStateId = table.Column<long>(type: "bigint", nullable: false, comment: "流程状态Id"),
                    ActionName = table.Column<string>(type: "longtext", nullable: true, comment: "动作名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActionForUserType = table.Column<int>(type: "int", nullable: false, comment: "动作使用者类型"),
                    ActionCode = table.Column<string>(type: "longtext", nullable: true, comment: "动作Code唯一编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowTargetStateId = table.Column<long>(type: "bigint", nullable: false, comment: "目标状态Id(当前动作执行完后的状态)"),
                    ActionType = table.Column<int>(type: "int", nullable: false, comment: "动作类型"),
                    IsEnable = table.Column<int>(type: "int", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_WorkFlowAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowAction_WorkFlowState_WorkFlowStateId",
                        column: x => x.WorkFlowStateId,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowAction_WorkFlowState_WorkFlowTargetStateId",
                        column: x => x.WorkFlowTargetStateId,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "工作流状态下的动作表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowStateConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StateId = table.Column<long>(type: "bigint", nullable: false, comment: "工作流状态Id"),
                    WorkFlowStateId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    RegionId = table.Column<long>(type: "bigint", nullable: false, comment: "工作流操作区域Id"),
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
                    table.PrimaryKey("PK_WorkFlowStateConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowStateConfig_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowStateConfig_WorkFlowState_WorkFlowStateId",
                        column: x => x.WorkFlowStateId,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id");
                },
                comment: "工作流状态配置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowActionLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkFlowActionId = table.Column<string>(type: "longtext", nullable: true, comment: "当前动作的Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowActionId1 = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false, comment: "业务实体Id"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "执行动作的用户Id"),
                    RegionId = table.Column<long>(type: "bigint", nullable: false, comment: "执行动作的区域Id"),
                    WorkFlowSourceStateIdId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    WorkFlowSourceStateId1 = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    WorkFlowTargetStateIdId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    WorkFlowTargetStateId1 = table.Column<long>(type: "bigint", nullable: true, comment: ""),
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
                    table.PrimaryKey("PK_WorkFlowActionLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActionLog_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowActionLog_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowActionLog_WorkFlowAction_WorkFlowActionId1",
                        column: x => x.WorkFlowActionId1,
                        principalTable: "WorkFlowAction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowActionLog_WorkFlowState_WorkFlowSourceStateId1",
                        column: x => x.WorkFlowSourceStateId1,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowActionLog_WorkFlowState_WorkFlowSourceStateIdId",
                        column: x => x.WorkFlowSourceStateIdId,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowActionLog_WorkFlowState_WorkFlowTargetStateId1",
                        column: x => x.WorkFlowTargetStateId1,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowActionLog_WorkFlowState_WorkFlowTargetStateIdId",
                        column: x => x.WorkFlowTargetStateIdId,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id");
                },
                comment: "工作流动作日志表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowActioncirculateConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkFlowActionId = table.Column<long>(type: "bigint", nullable: false, comment: "所在流程的动作Id"),
                    TargetRegionId = table.Column<long>(type: "bigint", nullable: false, comment: "目标所属区域Id"),
                    RegionId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    WorkFlowStateConfigId = table.Column<long>(type: "bigint", nullable: false, comment: "工作流 状态配置Id"),
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
                    table.PrimaryKey("PK_WorkFlowActioncirculateConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActioncirculateConfig_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowActioncirculateConfig_WorkFlowAction_WorkFlowActionId",
                        column: x => x.WorkFlowActionId,
                        principalTable: "WorkFlowAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowActioncirculateConfig_WorkFlowStateConfig_WorkFlowSt~",
                        column: x => x.WorkFlowStateConfigId,
                        principalTable: "WorkFlowStateConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "sub\r\n工作流动作的流转配置（不配置默认就是当前继续流转）")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowActionConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkFlowStateConfigId = table.Column<long>(type: "bigint", nullable: false, comment: "工作流状态配置Id"),
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
                    table.PrimaryKey("PK_WorkFlowActionConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActionConfig_WorkFlowStateConfig_WorkFlowStateConfig~",
                        column: x => x.WorkFlowStateConfigId,
                        principalTable: "WorkFlowStateConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "工作流动作配置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowStateLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkFlowActionLogId = table.Column<long>(type: "bigint", nullable: false, comment: "执行具体动作的日志Id"),
                    WorkFlowActionId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    WorkFlowStateId = table.Column<long>(type: "bigint", nullable: false, comment: "状态Id"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "当前操作用户Id"),
                    RegionId = table.Column<long>(type: "bigint", nullable: false, comment: "当前状态操作区域Id"),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false, comment: "业务实体Id"),
                    IsHistory = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否历史状态（0为当前状态 1为历史状态）"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "状态开始执行时间"),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "状态结束执行时间"),
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
                    table.PrimaryKey("PK_WorkFlowStateLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowStateLog_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowStateLog_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowStateLog_WorkFlowActionLog_WorkFlowActionLogId",
                        column: x => x.WorkFlowActionLogId,
                        principalTable: "WorkFlowActionLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowStateLog_WorkFlowAction_WorkFlowActionId",
                        column: x => x.WorkFlowActionId,
                        principalTable: "WorkFlowAction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowStateLog_WorkFlowState_WorkFlowStateId",
                        column: x => x.WorkFlowStateId,
                        principalTable: "WorkFlowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "工作流状态日志表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowAction_WorkFlowStateId",
                table: "WorkFlowAction",
                column: "WorkFlowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowAction_WorkFlowTargetStateId",
                table: "WorkFlowAction",
                column: "WorkFlowTargetStateId");

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

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionConfig_WorkFlowStateConfigId",
                table: "WorkFlowActionConfig",
                column: "WorkFlowStateConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_RegionId",
                table: "WorkFlowActionLog",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_UserId",
                table: "WorkFlowActionLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_WorkFlowActionId1",
                table: "WorkFlowActionLog",
                column: "WorkFlowActionId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_WorkFlowSourceStateId1",
                table: "WorkFlowActionLog",
                column: "WorkFlowSourceStateId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_WorkFlowSourceStateIdId",
                table: "WorkFlowActionLog",
                column: "WorkFlowSourceStateIdId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_WorkFlowTargetStateId1",
                table: "WorkFlowActionLog",
                column: "WorkFlowTargetStateId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActionLog_WorkFlowTargetStateIdId",
                table: "WorkFlowActionLog",
                column: "WorkFlowTargetStateIdId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowState_WorkFlowDefineId",
                table: "WorkFlowState",
                column: "WorkFlowDefineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateConfig_RegionId",
                table: "WorkFlowStateConfig",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateConfig_WorkFlowStateId",
                table: "WorkFlowStateConfig",
                column: "WorkFlowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateLog_RegionId",
                table: "WorkFlowStateLog",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateLog_UserId",
                table: "WorkFlowStateLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateLog_WorkFlowActionId",
                table: "WorkFlowStateLog",
                column: "WorkFlowActionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateLog_WorkFlowActionLogId",
                table: "WorkFlowStateLog",
                column: "WorkFlowActionLogId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStateLog_WorkFlowStateId",
                table: "WorkFlowStateLog",
                column: "WorkFlowStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkFlowActioncirculateConfig");

            migrationBuilder.DropTable(
                name: "WorkFlowActionConfig");

            migrationBuilder.DropTable(
                name: "WorkFlowAssignLog");

            migrationBuilder.DropTable(
                name: "WorkFlowDocument");

            migrationBuilder.DropTable(
                name: "WorkFlowFormDefaultConfig");

            migrationBuilder.DropTable(
                name: "WorkFlowStateLog");

            migrationBuilder.DropTable(
                name: "WorkFlowStateConfig");

            migrationBuilder.DropTable(
                name: "WorkFlowActionLog");

            migrationBuilder.DropTable(
                name: "WorkFlowAction");

            migrationBuilder.DropTable(
                name: "WorkFlowState");

            migrationBuilder.DropTable(
                name: "WorkFlowDefine");
        }
    }
}
