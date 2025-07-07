using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddataincolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Sessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Sessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Semesters",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Semesters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Semesters",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "sessionId",
                table: "Semesters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_sessionId",
                table: "Semesters",
                column: "sessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Sessions_sessionId",
                table: "Semesters",
                column: "sessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Sessions_sessionId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_sessionId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "sessionId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Programs");
        }
    }
}
