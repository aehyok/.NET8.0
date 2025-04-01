using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class InitWeChatUrlToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeChatUrlToBlog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SourceUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SourceUrlType = table.Column<int>(type: "int", nullable: false, comment: "Url来源分类"),
                    SourceContent = table.Column<string>(type: "longtext", nullable: true, comment: "含有html标签的微信公众号文章")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GeminiContent = table.Column<string>(type: "longtext", nullable: true, comment: "Gemini提取微信公众号文章")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReWriteContent = table.Column<string>(type: "longtext", nullable: true, comment: "重写内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConvertContentToHtml = table.Column<string>(type: "longtext", nullable: true, comment: "将重写内容转换为新的html风格")
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
                    table.PrimaryKey("PK_WeChatUrlToBlog", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeChatUrlToBlog");
        }
    }
}
