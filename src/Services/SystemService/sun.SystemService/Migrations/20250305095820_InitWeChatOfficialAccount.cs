using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class InitWeChatOfficialAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WeChatOfficialAccountId",
                table: "WeChatBlog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "WeChatOfficialAccount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "微信公众号名称")
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
                    table.PrimaryKey("PK_WeChatOfficialAccount", x => x.Id);
                },
                comment: "微信公众号列表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WeChatBlog_WeChatOfficialAccountId",
                table: "WeChatBlog",
                column: "WeChatOfficialAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeChatBlog_WeChatOfficialAccount_WeChatOfficialAccountId",
                table: "WeChatBlog",
                column: "WeChatOfficialAccountId",
                principalTable: "WeChatOfficialAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeChatBlog_WeChatOfficialAccount_WeChatOfficialAccountId",
                table: "WeChatBlog");

            migrationBuilder.DropTable(
                name: "WeChatOfficialAccount");

            migrationBuilder.DropIndex(
                name: "IX_WeChatBlog_WeChatOfficialAccountId",
                table: "WeChatBlog");

            migrationBuilder.DropColumn(
                name: "WeChatOfficialAccountId",
                table: "WeChatBlog");
        }
    }
}
