using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMediaUrlToBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaId",
                table: "WeChatUrlToBlog",
                type: "longtext",
                nullable: true,
                comment: "上传封面图返回Id")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "WeChatUrlToBlog",
                type: "longtext",
                nullable: true,
                comment: "封面图Url")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "WeChatUrlToBlog");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "WeChatUrlToBlog");
        }
    }
}
