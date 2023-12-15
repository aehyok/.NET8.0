using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.Schedules.Migrations
{
    /// <inheritdoc />
    public partial class SystemTableAddSystemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemId",
                table: "Tenant",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SystemId",
                table: "Region",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Options",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SystemId",
                table: "Menu",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SystemId",
                table: "DictionaryGroup",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Options_TenantId",
                table: "Options",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Tenant_TenantId",
                table: "Options",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Tenant_TenantId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_TenantId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "DictionaryGroup");
        }
    }
}
