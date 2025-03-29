using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class InitSystemPrompt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemPrompt",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(type: "longtext", nullable: true, comment: "提示词标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: true, comment: "提示词内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SystemPromptType = table.Column<int>(type: "int", nullable: false, comment: "分类"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_SystemPrompt", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemPrompt");
        }
    }
}
