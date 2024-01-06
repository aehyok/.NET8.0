using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCronTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserRole",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserRole",
                type: "bigint",
                nullable: true,
                comment: "");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "UserRole",
                type: "longtext",
                nullable: true,
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserRole",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "");

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserRole",
                type: "bigint",
                nullable: true,
                comment: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "CronTask",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastWriteTime",
                table: "CronTask",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskName",
                table: "CronTask",
                type: "longtext",
                nullable: true,
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_CreatedBy",
                table: "UserRole",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UpdatedBy",
                table: "UserRole",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_CreatedBy",
                table: "UserRole",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UpdatedBy",
                table: "UserRole",
                column: "UpdatedBy",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_CreatedBy",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UpdatedBy",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_CreatedBy",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UpdatedBy",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "CronTask");

            migrationBuilder.DropColumn(
                name: "LastWriteTime",
                table: "CronTask");

            migrationBuilder.DropColumn(
                name: "TaskName",
                table: "CronTask");
        }
    }
}
