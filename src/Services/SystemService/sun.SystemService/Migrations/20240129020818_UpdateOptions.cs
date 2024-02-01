using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Options",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Id 主键")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Options",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "创建时间");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Options",
                type: "bigint",
                nullable: true,
                comment: "创建人id");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Options",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否删除");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Options",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间");

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Options",
                type: "bigint",
                nullable: true,
                comment: "修改人id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Options");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Options",
                type: "bigint",
                nullable: false,
                comment: "Id 主键",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
