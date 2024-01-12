using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MenuResource",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "MenuResource",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "MenuResource",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MenuResource",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "MenuResource",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MenuResource");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MenuResource");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "MenuResource");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MenuResource");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MenuResource");
        }
    }
}
