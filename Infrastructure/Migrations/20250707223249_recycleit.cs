using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class recycleit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_students_StudentId",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Programs_StudentId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Programs");

            migrationBuilder.RenameColumn(
                name: "OthersName",
                table: "students",
                newName: "Religion");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "students",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "TotalCAMark",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalExamMark",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "students",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalGovnmentId",
                table: "students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MarialStatus",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_students_LocalGovnmentId",
                table: "students",
                column: "LocalGovnmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_localGovnments_LocalGovnmentId",
                table: "students",
                column: "LocalGovnmentId",
                principalTable: "localGovnments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_localGovnments_LocalGovnmentId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_students_LocalGovnmentId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "TotalCAMark",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TotalExamMark",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "students");

            migrationBuilder.DropColumn(
                name: "LocalGovnmentId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "MarialStatus",
                table: "students");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "Religion",
                table: "students",
                newName: "OthersName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "students",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Programs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_StudentId",
                table: "Programs",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_students_StudentId",
                table: "Programs",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_classes_ClassId",
                table: "Subjects",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
