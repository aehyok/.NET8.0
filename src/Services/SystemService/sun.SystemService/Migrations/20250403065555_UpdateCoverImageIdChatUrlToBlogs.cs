using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCoverImageIdChatUrlToBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CoverImageId",
                table: "WeChatUrlToBlog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "生成的封面图Id",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "生成的封面图Id")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoverImageId",
                table: "WeChatUrlToBlog",
                type: "longtext",
                nullable: true,
                comment: "生成的封面图Id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "生成的封面图Id")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
