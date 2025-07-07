using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateclasstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TecherId",
                table: "classes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_classes_TecherId",
                table: "classes",
                column: "TecherId");

            migrationBuilder.AddForeignKey(
                name: "FK_classes_employees_TecherId",
                table: "classes",
                column: "TecherId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classes_employees_TecherId",
                table: "classes");

            migrationBuilder.DropIndex(
                name: "IX_classes_TecherId",
                table: "classes");

            migrationBuilder.DropColumn(
                name: "TecherId",
                table: "classes");
        }
    }
}
