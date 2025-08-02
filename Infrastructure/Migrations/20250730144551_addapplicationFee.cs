using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addapplicationFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClassId1",
                table: "StudentPrograms",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ApplicationFee",
                table: "Programs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RelationShip",
                table: "parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPrograms_ClassId1",
                table: "StudentPrograms",
                column: "ClassId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPrograms_classes_ClassId1",
                table: "StudentPrograms",
                column: "ClassId1",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPrograms_classes_ClassId1",
                table: "StudentPrograms");

            migrationBuilder.DropIndex(
                name: "IX_StudentPrograms_ClassId1",
                table: "StudentPrograms");

            migrationBuilder.DropColumn(
                name: "ClassId1",
                table: "StudentPrograms");

            migrationBuilder.DropColumn(
                name: "ApplicationFee",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "RelationShip",
                table: "parents");
        }
    }
}
