using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class edittablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Sessions_sessionId",
                table: "Semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "Terms");

            migrationBuilder.RenameIndex(
                name: "IX_Semesters_sessionId",
                table: "Terms",
                newName: "IX_Terms_sessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Terms",
                table: "Terms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_Sessions_sessionId",
                table: "Terms",
                column: "sessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terms_Sessions_sessionId",
                table: "Terms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Terms",
                table: "Terms");

            migrationBuilder.RenameTable(
                name: "Terms",
                newName: "Semesters");

            migrationBuilder.RenameIndex(
                name: "IX_Terms_sessionId",
                table: "Semesters",
                newName: "IX_Semesters_sessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Sessions_sessionId",
                table: "Semesters",
                column: "sessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
