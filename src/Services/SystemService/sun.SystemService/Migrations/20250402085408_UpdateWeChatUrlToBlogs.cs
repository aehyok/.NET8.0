using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWeChatUrlToBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageId",
                table: "WeChatUrlToBlog",
                type: "longtext",
                nullable: true,
                comment: "生成的封面图Id")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "WeChatUrlToBlog",
                type: "longtext",
                nullable: true,
                comment: "封面图文件名")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "WeChatUrlToBlog",
                type: "longtext",
                nullable: true,
                comment: "生成的文章标题")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "WeChatUrlToBlog");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "WeChatUrlToBlog");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "WeChatUrlToBlog");
        }
    }
}
