using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnsinstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "states",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "states",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "localGovnments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "localGovnments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_localGovnments_StateId",
                table: "localGovnments",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_localGovnments_states_StateId",
                table: "localGovnments",
                column: "StateId",
                principalTable: "states",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_localGovnments_states_StateId",
                table: "localGovnments");

            migrationBuilder.DropIndex(
                name: "IX_localGovnments_StateId",
                table: "localGovnments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "states");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "states");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "localGovnments");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "localGovnments");
        }
    }
}
