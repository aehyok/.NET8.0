using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
