using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApiResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApiResource",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "ApiResource",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "ApiResource",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ApiResource",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "ApiResource",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApiResource");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ApiResource");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "ApiResource");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ApiResource");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ApiResource");
        }
    }
}
