using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class CollectFormAndGuideLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectFormMetaData",
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
                    Status = table.Column<int>(type: "int", nullable: false, comment: "1正常，2停用"),
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
                    zbmc = table.Column<string>(type: "longtext", nullable: true, comment: "指标名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbzt = table.Column<string>(type: "longtext", nullable: true, comment: "指标主题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbsf = table.Column<string>(type: "longtext", nullable: true, comment: "指标算法")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbmeta = table.Column<string>(type: "longtext", nullable: true, comment: "指标元数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fid = table.Column<long>(type: "bigint", nullable: false, comment: "父级Id"),
                    zbcxsf = table.Column<string>(type: "longtext", nullable: true, comment: "指标查询算法 指标查询的SELECT语句")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    jsmx_zbmeta = table.Column<string>(type: "longtext", nullable: true, comment: "明细_指标元数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    xsxh = table.Column<string>(type: "longtext", nullable: true, comment: "显示顺序")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zbsm = table.Column<string>(type: "longtext", nullable: true, comment: "指标说明")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    md5 = table.Column<string>(type: "longtext", nullable: true, comment: "MD5值")
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
                    table.PrimaryKey("PK_CollectFormMetaDataLine", x => x.Id);
                },
                comment: "form表单自动生成sql的指标定义表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectFormMetaData");

            migrationBuilder.DropTable(
                name: "CollectFormMetaDataLine");
        }
    }
}
