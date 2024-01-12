using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aehyok.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOperationLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Operation",
                table: "OperationLog",
                newName: "OperationContent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperationContent",
                table: "OperationLog",
                newName: "Operation");
        }
    }
}
