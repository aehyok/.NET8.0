using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApiResource",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "接口名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "接口标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "所有接口按 Controller 分组，分组名称为 Controller 注释")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoutePattern = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "路由匹配模式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameSpace = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "命名空间")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ControllerName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "控制器名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActionName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "操作名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestMethod = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "请求方式")
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
                    table.PrimaryKey("PK_ApiResource", x => x.Id);
                },
                comment: "接口资源")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AsyncTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(type: "int", nullable: false, comment: "任务状态"),
                    Code = table.Column<string>(type: "longtext", nullable: true, comment: "任务标识，根据该值判断处理方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<string>(type: "longtext", nullable: true, comment: "任务数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ErrorMessage = table.Column<string>(type: "longtext", nullable: true, comment: "错误消息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Result = table.Column<string>(type: "longtext", nullable: true, comment: "任务返回数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RetryCount = table.Column<int>(type: "int", nullable: false, comment: "重试次数"),
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
                    table.PrimaryKey("PK_AsyncTask", x => x.Id);
                },
                comment: "异步任务")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoDictionary",
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
                    table.PrimaryKey("PK_AutoDictionary", x => x.Id);
                },
                comment: "自动化字典")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoForm",
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
                    table.PrimaryKey("PK_AutoGuideLine", x => x.Id);
                },
                comment: "自动化指标表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoRecord",
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
                    table.PrimaryKey("PK_AutoRecord", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoRecordLog",
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
                    table.PrimaryKey("PK_AutoRecordLog", x => x.Id);
                },
                comment: "自动化填报记录日志")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
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
                    table.PrimaryKey("PK_AutoTask", x => x.Id);
                },
                comment: "自动化任务表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoTaskLog",
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
                    table.PrimaryKey("PK_AutoTaskLog", x => x.Id);
                },
                comment: "自动化任务日志")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoTaskRelation",
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
                    table.PrimaryKey("PK_AutoTaskRelation", x => x.Id);
                },
                comment: "自动化任务关联关系")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DictionaryGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsSystem = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "系统字典分组，无法编辑"),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: "所属系统Id"),
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
                    table.PrimaryKey("PK_DictionaryGroup", x => x.Id);
                },
                comment: "字典分组")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "文件名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "文件类型"),
                    Size = table.Column<long>(type: "bigint", nullable: false, comment: "文件大小"),
                    Extension = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, comment: "文件扩展名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "文件 Url")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "文件存储路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageType = table.Column<int>(type: "int", nullable: false, comment: "文件存储方式"),
                    ContentType = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "文件MIME")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hash = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "文件 MD5 Hash 值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalId = table.Column<long>(type: "bigint", nullable: false, comment: "原文件 Id, 用于存储缩略图、裁剪图、视频转码等文件的原始文件编号，如果为 0 表示该文件为原始文件"),
                    State = table.Column<int>(type: "int", nullable: false, comment: "文件状态"),
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
                    table.PrimaryKey("PK_File", x => x.Id);
                },
                comment: "文件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "菜单名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "菜单标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: "父级菜单"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "菜单类型"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "所属平台类型"),
                    Icon = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IconType = table.Column<int>(type: "int", nullable: false, comment: "菜单图标类型"),
                    ActiveIcon = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "选中图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActiveIconType = table.Column<int>(type: "int", nullable: false, comment: "菜单选中图标类型"),
                    Url = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsVisible = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否可见"),
                    IsExternalLink = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否外链"),
                    IdSequences = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "Id 序列")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: "所属系统Id"),
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
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "菜单")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MessageQueueEventHandler",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageQueueEventHandler", x => x.Id);
                },
                comment: "RabbitMQ消息队列事件处理器")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "简称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "别名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: "父级编号，无父级为 0"),
                    Level = table.Column<int>(type: "int", nullable: false, comment: "层级"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "显示排序"),
                    IdSequences = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, comment: "Id 序列")
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
                    table.PrimaryKey("PK_Region", x => x.Id);
                },
                comment: "行政区域")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "所属平台类型"),
                    Code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "别名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSystem = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否系统内置角色"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "显示排序"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: "所属系统Id"),
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
                    table.PrimaryKey("PK_Role", x => x.Id);
                },
                comment: "角色")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SeedDataTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastWriteTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后一次的文件修改时间"),
                    TaskName = table.Column<string>(type: "longtext", nullable: true, comment: "任务名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    ExecuteTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "执行时间"),
                    ExecuteStatus = table.Column<int>(type: "int", nullable: false, comment: "任务执行状态"),
                    ConfigPath = table.Column<string>(type: "longtext", nullable: true, comment: "配置文件地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedDataTask", x => x.Id);
                },
                comment: "种子数据更新任务")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "系统名称")
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
                    table.PrimaryKey("PK_System", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentType = table.Column<int>(type: "int", nullable: false, comment: "内容类型"),
                    Content = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Variable = table.Column<string>(type: "longtext", nullable: true, comment: "变量")
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
                    table.PrimaryKey("PK_Template", x => x.Id);
                },
                comment: "模板")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "租户名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppId = table.Column<string>(type: "longtext", nullable: true, comment: "AppId")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppSecret = table.Column<string>(type: "longtext", nullable: true, comment: "App秘钥")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: "所属系统Id"),
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
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                },
                comment: "系统租户表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuResource",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuId = table.Column<long>(type: "bigint", nullable: false, comment: "菜单编号"),
                    ApiResourceId = table.Column<long>(type: "bigint", nullable: false, comment: "接口编号"),
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
                    table.PrimaryKey("PK_MenuResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuResource_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuResource_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "菜单与接口关联数据")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false, comment: "权限所属对象编号"),
                    MenuId = table.Column<long>(type: "bigint", nullable: false, comment: "菜单编号"),
                    DataRange = table.Column<int>(type: "int", nullable: false, comment: "权限数据范围"),
                    HasPermission = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否拥有权限"),
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
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "权限表RoleMenu(角色下勾选的所有菜单)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Mobile = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, comment: "手机号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "真实姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "用户名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NickName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "昵称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordSalt = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "Password Salt")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "性别"),
                    Avatar = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后登录时间"),
                    SignatureUrl = table.Column<string>(type: "longtext", nullable: true, comment: "签名url")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
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
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                },
                comment: "用户")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SystemRegion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: "系统Id"),
                    RegionId = table.Column<long>(type: "bigint", nullable: false, comment: "区域Id"),
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
                    table.PrimaryKey("PK_SystemRegion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemRegion_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemRegion_System_SystemId",
                        column: x => x.SystemId,
                        principalTable: "System",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "系统勾选区域Id")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Id 主键")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "text", nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenantId = table.Column<long>(type: "bigint", nullable: false, comment: "租户Id"),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: "所属系统Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "系统配置参数项")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlatformOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "平台名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logo = table.Column<string>(type: "longtext", nullable: true, comment: "平台 Logo")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Theme = table.Column<string>(type: "longtext", nullable: true, comment: "平台主题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<double>(type: "double", nullable: false, comment: "经度"),
                    Latitude = table.Column<double>(type: "double", nullable: false, comment: "纬度"),
                    LoginImage = table.Column<string>(type: "longtext", nullable: true, comment: "登录页背景")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenantId = table.Column<long>(type: "bigint", nullable: false, comment: "租户Id"),
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
                    table.PrimaryKey("PK_PlatformOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformOptions_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "租户平台配置项")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DictionaryItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DictionaryGroupId = table.Column<long>(type: "bigint", nullable: false, comment: "字典分组"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: "父级字典编号"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    IsVisible = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否可见"),
                    IsSystem = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "系统字典，应无法编辑修改"),
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
                    table.PrimaryKey("PK_DictionaryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryItem_DictionaryGroup_DictionaryGroupId",
                        column: x => x.DictionaryGroupId,
                        principalTable: "DictionaryGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DictionaryItem_DictionaryItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DictionaryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DictionaryItem_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DictionaryItem_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "字典项")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OperationLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IpAddress = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "IP 地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAgent = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "User Agent")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperationContent = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "操作")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MenuCode = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "菜单代码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperationMenu = table.Column<string>(type: "longtext", nullable: true, comment: "操作菜单")
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
                    table.PrimaryKey("PK_OperationLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationLog_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "操作日志")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "用户编号"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false, comment: "角色编号"),
                    RegionId = table.Column<long>(type: "bigint", nullable: false, comment: "所属行政区域编号"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否默认角色"),
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
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "用户角色关联信息")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "用户编号"),
                    Token = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "Token")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenHash = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "Token Hash 用于查询")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "Refresh Token")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "过期时间"),
                    IpAddress = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "获取 Token IP 地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAgent = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "获取 Token UserAgent")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "当前获取 Token 平台"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false, comment: "当前登录的角色编号"),
                    RegionId = table.Column<long>(type: "bigint", nullable: false, comment: "当前登录的区域编号"),
                    LoginMethodType = table.Column<int>(type: "int", nullable: false, comment: "登录方式"),
                    RefreshTokenIsAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "Refresh Token 是否有效"),
                    LoginType = table.Column<int>(type: "int", nullable: false, comment: "登录方式"),
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
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToken_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "用户 Token")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItem_CreatedBy",
                table: "DictionaryItem",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItem_DictionaryGroupId",
                table: "DictionaryItem",
                column: "DictionaryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItem_ParentId",
                table: "DictionaryItem",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItem_UpdatedBy",
                table: "DictionaryItem",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuResource_ApiResourceId",
                table: "MenuResource",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuResource_MenuId",
                table: "MenuResource",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLog_CreatedBy",
                table: "OperationLog",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Options_TenantId",
                table: "Options",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_MenuId",
                table: "Permission",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_RoleId",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformOptions_TenantId",
                table: "PlatformOptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRegion_RegionId",
                table: "SystemRegion",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRegion_SystemId",
                table: "SystemRegion",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RegionId",
                table: "UserRole",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_RegionId",
                table: "UserToken",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_RoleId",
                table: "UserToken",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                table: "UserToken",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsyncTask");

            migrationBuilder.DropTable(
                name: "AutoDictionary");

            migrationBuilder.DropTable(
                name: "AutoForm");

            migrationBuilder.DropTable(
                name: "AutoGuideLine");

            migrationBuilder.DropTable(
                name: "AutoRecord");

            migrationBuilder.DropTable(
                name: "AutoRecordLog");

            migrationBuilder.DropTable(
                name: "AutoTask");

            migrationBuilder.DropTable(
                name: "AutoTaskLog");

            migrationBuilder.DropTable(
                name: "AutoTaskRelation");

            migrationBuilder.DropTable(
                name: "DictionaryItem");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "MenuResource");

            migrationBuilder.DropTable(
                name: "MessageQueueEventHandler");

            migrationBuilder.DropTable(
                name: "OperationLog");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "PlatformOptions");

            migrationBuilder.DropTable(
                name: "SeedDataTask");

            migrationBuilder.DropTable(
                name: "SystemRegion");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "DictionaryGroup");

            migrationBuilder.DropTable(
                name: "ApiResource");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "System");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
