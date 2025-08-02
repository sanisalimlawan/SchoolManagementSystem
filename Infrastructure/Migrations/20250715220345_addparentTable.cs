using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addparentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarialStatus",
                table: "students");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "parents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModefiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_ParentId",
                table: "students",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_parents_ParentId",
                table: "students",
                column: "ParentId",
                principalTable: "parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_parents_ParentId",
                table: "students");

            migrationBuilder.DropTable(
                name: "parents");

            migrationBuilder.DropIndex(
                name: "IX_students_ParentId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "students");

            migrationBuilder.AddColumn<int>(
                name: "MarialStatus",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
