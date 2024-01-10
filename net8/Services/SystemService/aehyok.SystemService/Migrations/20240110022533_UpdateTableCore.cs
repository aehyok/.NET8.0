using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoDictionary_User_CreatedBy",
            //    table: "AutoDictionary");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoDictionary_User_UpdatedBy",
            //    table: "AutoDictionary");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoForm_User_CreatedBy",
            //    table: "AutoForm");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoForm_User_UpdatedBy",
            //    table: "AutoForm");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoGuideLine_User_CreatedBy",
            //    table: "AutoGuideLine");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoGuideLine_User_UpdatedBy",
            //    table: "AutoGuideLine");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoRecord_User_CreatedBy",
            //    table: "AutoRecord");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoRecord_User_UpdatedBy",
            //    table: "AutoRecord");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoRecordLog_User_CreatedBy",
            //    table: "AutoRecordLog");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoRecordLog_User_UpdatedBy",
            //    table: "AutoRecordLog");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoTask_User_CreatedBy",
            //    table: "AutoTask");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoTask_User_UpdatedBy",
            //    table: "AutoTask");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoTaskLog_User_CreatedBy",
            //    table: "AutoTaskLog");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoTaskLog_User_UpdatedBy",
            //    table: "AutoTaskLog");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoTaskRelation_User_CreatedBy",
            //    table: "AutoTaskRelation");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoTaskRelation_User_UpdatedBy",
            //    table: "AutoTaskRelation");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_File_User_CreatedBy",
            //    table: "File");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_File_User_UpdatedBy",
            //    table: "File");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PlatformOptions_User_CreatedBy",
            //    table: "PlatformOptions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PlatformOptions_User_UpdatedBy",
            //    table: "PlatformOptions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_SystemRegion_User_CreatedBy",
            //    table: "SystemRegion");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_SystemRegion_User_UpdatedBy",
            //    table: "SystemRegion");

            migrationBuilder.DropIndex(
                name: "IX_SystemRegion_CreatedBy",
                table: "SystemRegion");

            migrationBuilder.DropIndex(
                name: "IX_SystemRegion_UpdatedBy",
                table: "SystemRegion");

            migrationBuilder.DropIndex(
                name: "IX_PlatformOptions_CreatedBy",
                table: "PlatformOptions");

            migrationBuilder.DropIndex(
                name: "IX_PlatformOptions_UpdatedBy",
                table: "PlatformOptions");

            migrationBuilder.DropIndex(
                name: "IX_File_CreatedBy",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_UpdatedBy",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_AutoTaskRelation_CreatedBy",
                table: "AutoTaskRelation");

            migrationBuilder.DropIndex(
                name: "IX_AutoTaskRelation_UpdatedBy",
                table: "AutoTaskRelation");

            migrationBuilder.DropIndex(
                name: "IX_AutoTaskLog_CreatedBy",
                table: "AutoTaskLog");

            migrationBuilder.DropIndex(
                name: "IX_AutoTaskLog_UpdatedBy",
                table: "AutoTaskLog");

            migrationBuilder.DropIndex(
                name: "IX_AutoTask_CreatedBy",
                table: "AutoTask");

            migrationBuilder.DropIndex(
                name: "IX_AutoTask_UpdatedBy",
                table: "AutoTask");

            migrationBuilder.DropIndex(
                name: "IX_AutoRecordLog_CreatedBy",
                table: "AutoRecordLog");

            migrationBuilder.DropIndex(
                name: "IX_AutoRecordLog_UpdatedBy",
                table: "AutoRecordLog");

            migrationBuilder.DropIndex(
                name: "IX_AutoRecord_CreatedBy",
                table: "AutoRecord");

            migrationBuilder.DropIndex(
                name: "IX_AutoRecord_UpdatedBy",
                table: "AutoRecord");

            migrationBuilder.DropIndex(
                name: "IX_AutoGuideLine_CreatedBy",
                table: "AutoGuideLine");

            migrationBuilder.DropIndex(
                name: "IX_AutoGuideLine_UpdatedBy",
                table: "AutoGuideLine");

            migrationBuilder.DropIndex(
                name: "IX_AutoForm_CreatedBy",
                table: "AutoForm");

            migrationBuilder.DropIndex(
                name: "IX_AutoForm_UpdatedBy",
                table: "AutoForm");

            migrationBuilder.DropIndex(
                name: "IX_AutoDictionary_CreatedBy",
                table: "AutoDictionary");

            migrationBuilder.DropIndex(
                name: "IX_AutoDictionary_UpdatedBy",
                table: "AutoDictionary");

            migrationBuilder.AlterTable(
                name: "SeedDataTask",
                comment: "种子数据更新任务",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "MessageQueueEventHandler",
                comment: "RabbitMQ消息队列事件处理器",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoTaskRelation",
                comment: "自动化任务关联关系",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoTaskLog",
                comment: "自动化任务日志",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoTask",
                comment: "自动化任务表",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoRecordLog",
                comment: "自动化填报记录日志",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoRecord",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoGuideLine",
                comment: "自动化指标表",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoForm",
                comment: "自定义表单表",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoDictionary",
                comment: "自动化字典",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "ApiResource",
                comment: "接口资源",
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "UserToken",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserToken",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "UserToken",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserToken",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "UserToken",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserToken",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserToken",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "UserRole",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserRole",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "UserRole",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserRole",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "UserRole",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserRole",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserRole",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "User",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "User",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "User",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "User",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Tenant",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tenant",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Tenant",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Tenant",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Tenant",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tenant",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Tenant",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "SystemRegion",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "SystemRegion",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SystemRegion",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SystemRegion",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "SystemRegion",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SystemRegion",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SystemRegion",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "System",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "System",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "System",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "System",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "System",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "System",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "System",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "TaskName",
                table: "SeedDataTask",
                type: "longtext",
                nullable: true,
                comment: "任务名称",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastWriteTime",
                table: "SeedDataTask",
                type: "datetime(6)",
                nullable: false,
                comment: "最后一次的文件修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "SeedDataTask",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SeedDataTask",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecuteTime",
                table: "SeedDataTask",
                type: "datetime(6)",
                nullable: false,
                comment: "执行时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<int>(
                name: "ExecuteStatus",
                table: "SeedDataTask",
                type: "int",
                nullable: false,
                comment: "任务执行状态",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "ConfigPath",
                table: "SeedDataTask",
                type: "longtext",
                nullable: true,
                comment: "配置文件地址",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SeedDataTask",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Role",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Role",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Role",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Role",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Role",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Role",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Region",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Region",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Region",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Region",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Region",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Region",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Region",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "PlatformOptions",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "PlatformOptions",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "PlatformOptions",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PlatformOptions",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "PlatformOptions",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PlatformOptions",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "PlatformOptions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Permission",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Permission",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Permission",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Permission",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Permission",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Permission",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Permission",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Options",
                type: "bigint",
                nullable: false,
                comment: "Id 主键",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MessageQueueEventHandler",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "MessageQueueEventHandler",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "MenuResource",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "MenuResource",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "MenuResource",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MenuResource",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "MenuResource",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "MenuResource",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "MenuResource",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Menu",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Menu",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Menu",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Menu",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Menu",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menu",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Menu",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "File",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "File",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "File",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "File",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "File",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "File",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "File",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "DictionaryItem",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "DictionaryItem",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "DictionaryItem",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DictionaryItem",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "DictionaryItem",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DictionaryItem",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "DictionaryItem",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "DictionaryGroup",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "DictionaryGroup",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "DictionaryGroup",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DictionaryGroup",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "DictionaryGroup",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DictionaryGroup",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "DictionaryGroup",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoTaskRelation",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoTaskRelation",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoTaskRelation",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoTaskRelation",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoTaskRelation",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoTaskRelation",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoTaskRelation",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoTaskLog",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoTaskLog",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoTaskLog",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoTaskLog",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoTaskLog",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoTaskLog",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoTaskLog",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoTask",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoTask",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoTask",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AutoTask",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoTask",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoTask",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoTask",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoTask",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoRecordLog",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoRecordLog",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoRecordLog",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoRecordLog",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoRecordLog",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoRecordLog",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoRecordLog",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoRecord",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoRecord",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoRecord",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoRecord",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoRecord",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoRecord",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoRecord",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoGuideLine",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoGuideLine",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoGuideLine",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoGuideLine",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoGuideLine",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoGuideLine",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoGuideLine",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoForm",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoForm",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoForm",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoForm",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoForm",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoForm",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoForm",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoDictionary",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoDictionary",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoDictionary",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoDictionary",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoDictionary",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoDictionary",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoDictionary",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "ApiResource",
                type: "bigint",
                nullable: true,
                comment: "修改人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ApiResource",
                type: "datetime(6)",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "RoutePattern",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "路由匹配模式",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RequestMethod",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "请求方式",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "ApiResource",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "NameSpace",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "命名空间",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "接口名称",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ApiResource",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "所有接口按 Controller 分组，分组名称为 Controller 注释",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "ApiResource",
                type: "bigint",
                nullable: true,
                comment: "创建人id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ApiResource",
                type: "datetime(6)",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "");

            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "控制器名称",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "接口标识",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "操作名称",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ApiResource",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "SeedDataTask",
                comment: "",
                oldComment: "种子数据更新任务")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "MessageQueueEventHandler",
                comment: "",
                oldComment: "RabbitMQ消息队列事件处理器")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoTaskRelation",
                comment: "",
                oldComment: "自动化任务关联关系")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoTaskLog",
                comment: "",
                oldComment: "自动化任务日志")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoTask",
                comment: "",
                oldComment: "自动化任务表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoRecordLog",
                comment: "",
                oldComment: "自动化填报记录日志")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoRecord",
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoGuideLine",
                comment: "",
                oldComment: "自动化指标表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoForm",
                comment: "",
                oldComment: "自定义表单表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "AutoDictionary",
                comment: "",
                oldComment: "自动化字典")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "ApiResource",
                comment: "",
                oldComment: "接口资源")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "UserToken",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserToken",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "UserToken",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserToken",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "UserToken",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserToken",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserToken",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "UserRole",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserRole",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "UserRole",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserRole",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "UserRole",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserRole",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserRole",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "User",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "User",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "User",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "User",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Tenant",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tenant",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Tenant",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Tenant",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Tenant",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tenant",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Tenant",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "SystemRegion",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "SystemRegion",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SystemRegion",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SystemRegion",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "SystemRegion",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SystemRegion",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SystemRegion",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "System",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "System",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "System",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "System",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "System",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "System",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "System",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "TaskName",
                table: "SeedDataTask",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "任务名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastWriteTime",
                table: "SeedDataTask",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "最后一次的文件修改时间");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "SeedDataTask",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SeedDataTask",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecuteTime",
                table: "SeedDataTask",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "执行时间");

            migrationBuilder.AlterColumn<int>(
                name: "ExecuteStatus",
                table: "SeedDataTask",
                type: "int",
                nullable: false,
                comment: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "任务执行状态");

            migrationBuilder.AlterColumn<string>(
                name: "ConfigPath",
                table: "SeedDataTask",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "配置文件地址")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SeedDataTask",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Role",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Role",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Role",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Role",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Role",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Role",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Role",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Region",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Region",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Region",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Region",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Region",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Region",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Region",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "PlatformOptions",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "PlatformOptions",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "PlatformOptions",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PlatformOptions",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "PlatformOptions",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PlatformOptions",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "PlatformOptions",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Permission",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Permission",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Permission",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Permission",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Permission",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Permission",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Permission",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Options",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Id 主键")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MessageQueueEventHandler",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "MessageQueueEventHandler",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "MenuResource",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "MenuResource",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "MenuResource",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MenuResource",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "MenuResource",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "MenuResource",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "MenuResource",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Menu",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Menu",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Menu",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Menu",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Menu",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menu",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Menu",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "File",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "File",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "File",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "File",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "File",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "File",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "File",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "DictionaryItem",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "DictionaryItem",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "DictionaryItem",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DictionaryItem",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "DictionaryItem",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DictionaryItem",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "DictionaryItem",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "DictionaryGroup",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "DictionaryGroup",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "DictionaryGroup",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DictionaryGroup",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "DictionaryGroup",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DictionaryGroup",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "DictionaryGroup",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoTaskRelation",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoTaskRelation",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoTaskRelation",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoTaskRelation",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoTaskRelation",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoTaskRelation",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoTaskRelation",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoTaskLog",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoTaskLog",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoTaskLog",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoTaskLog",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoTaskLog",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoTaskLog",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoTaskLog",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoTask",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoTask",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoTask",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AutoTask",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoTask",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoTask",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoTask",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoTask",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoRecordLog",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoRecordLog",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoRecordLog",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoRecordLog",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoRecordLog",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoRecordLog",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoRecordLog",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoRecord",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoRecord",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoRecord",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoRecord",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoRecord",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoRecord",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoRecord",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoGuideLine",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoGuideLine",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoGuideLine",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoGuideLine",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoGuideLine",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoGuideLine",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoGuideLine",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoForm",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoForm",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoForm",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoForm",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoForm",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoForm",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoForm",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "AutoDictionary",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AutoDictionary",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "AutoDictionary",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AutoDictionary",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "AutoDictionary",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AutoDictionary",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AutoDictionary",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "ApiResource",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "修改人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ApiResource",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<string>(
                name: "RoutePattern",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "路由匹配模式")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RequestMethod",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "请求方式")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "ApiResource",
                type: "longtext",
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "NameSpace",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "命名空间")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "接口名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ApiResource",
                type: "tinyint(1)",
                nullable: false,
                comment: "",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "所有接口按 Controller 分组，分组名称为 Controller 注释")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "ApiResource",
                type: "bigint",
                nullable: true,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "创建人id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ApiResource",
                type: "datetime(6)",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "控制器名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "接口标识")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "ApiResource",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "操作名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ApiResource",
                type: "bigint",
                nullable: false,
                comment: "",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_SystemRegion_CreatedBy",
                table: "SystemRegion",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRegion_UpdatedBy",
                table: "SystemRegion",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformOptions_CreatedBy",
                table: "PlatformOptions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformOptions_UpdatedBy",
                table: "PlatformOptions",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_File_CreatedBy",
                table: "File",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_File_UpdatedBy",
                table: "File",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoTaskRelation_CreatedBy",
                table: "AutoTaskRelation",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoTaskRelation_UpdatedBy",
                table: "AutoTaskRelation",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoTaskLog_CreatedBy",
                table: "AutoTaskLog",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoTaskLog_UpdatedBy",
                table: "AutoTaskLog",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoTask_CreatedBy",
                table: "AutoTask",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoTask_UpdatedBy",
                table: "AutoTask",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRecordLog_CreatedBy",
                table: "AutoRecordLog",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRecordLog_UpdatedBy",
                table: "AutoRecordLog",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRecord_CreatedBy",
                table: "AutoRecord",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRecord_UpdatedBy",
                table: "AutoRecord",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoGuideLine_CreatedBy",
                table: "AutoGuideLine",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoGuideLine_UpdatedBy",
                table: "AutoGuideLine",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoForm_CreatedBy",
                table: "AutoForm",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoForm_UpdatedBy",
                table: "AutoForm",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoDictionary_CreatedBy",
                table: "AutoDictionary",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AutoDictionary_UpdatedBy",
                table: "AutoDictionary",
                column: "UpdatedBy");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoDictionary_User_CreatedBy",
            //    table: "AutoDictionary",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoDictionary_User_UpdatedBy",
            //    table: "AutoDictionary",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoForm_User_CreatedBy",
            //    table: "AutoForm",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoForm_User_UpdatedBy",
            //    table: "AutoForm",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoGuideLine_User_CreatedBy",
            //    table: "AutoGuideLine",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoGuideLine_User_UpdatedBy",
            //    table: "AutoGuideLine",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoRecord_User_CreatedBy",
            //    table: "AutoRecord",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoRecord_User_UpdatedBy",
            //    table: "AutoRecord",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoRecordLog_User_CreatedBy",
            //    table: "AutoRecordLog",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoRecordLog_User_UpdatedBy",
            //    table: "AutoRecordLog",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoTask_User_CreatedBy",
            //    table: "AutoTask",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoTask_User_UpdatedBy",
            //    table: "AutoTask",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoTaskLog_User_CreatedBy",
            //    table: "AutoTaskLog",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoTaskLog_User_UpdatedBy",
            //    table: "AutoTaskLog",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoTaskRelation_User_CreatedBy",
            //    table: "AutoTaskRelation",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoTaskRelation_User_UpdatedBy",
            //    table: "AutoTaskRelation",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_File_User_CreatedBy",
            //    table: "File",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_File_User_UpdatedBy",
            //    table: "File",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PlatformOptions_User_CreatedBy",
            //    table: "PlatformOptions",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PlatformOptions_User_UpdatedBy",
            //    table: "PlatformOptions",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SystemRegion_User_CreatedBy",
            //    table: "SystemRegion",
            //    column: "CreatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SystemRegion_User_UpdatedBy",
            //    table: "SystemRegion",
            //    column: "UpdatedBy",
            //    principalTable: "User",
            //    principalColumn: "Id");
        }
    }
}
