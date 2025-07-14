using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatesubjec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectTeacherId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectTeacherId",
                table: "Subjects",
                column: "SubjectTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_employees_SubjectTeacherId",
                table: "Subjects",
                column: "SubjectTeacherId",
                principalTable: "employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_employees_SubjectTeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubjectTeacherId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectTeacherId",
                table: "Subjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
