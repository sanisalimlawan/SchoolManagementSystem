using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class somemodefication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassId",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPrograms",
                table: "StudentPrograms");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "students");

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genotype",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalConditions",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegNumber",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "StudentPrograms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "parents",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "parents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "parents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPrograms",
                table: "StudentPrograms",
                columns: new[] { "StudentId", "ProgramId", "ClassId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPrograms_ClassId",
                table: "StudentPrograms",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPrograms_classes_ClassId",
                table: "StudentPrograms",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPrograms_classes_ClassId",
                table: "StudentPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPrograms",
                table: "StudentPrograms");

            migrationBuilder.DropIndex(
                name: "IX_StudentPrograms_ClassId",
                table: "StudentPrograms");

            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "students");

            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Genotype",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "students");

            migrationBuilder.DropColumn(
                name: "MedicalConditions",
                table: "students");

            migrationBuilder.DropColumn(
                name: "RegNumber",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "StudentPrograms");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "parents");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPrograms",
                table: "StudentPrograms",
                columns: new[] { "StudentId", "ProgramId" });

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassId",
                table: "students",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id");
        }
    }
}
