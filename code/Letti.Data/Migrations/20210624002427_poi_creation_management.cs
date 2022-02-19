using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class poi_creation_management : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "PersonOfInterests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PersonOfInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PersonOfInterests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 06, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsOficial",
                table: "PersonOfInterests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "PersonOfInterests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PersonOfInterests");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PersonOfInterests");

            migrationBuilder.DropColumn(
                name: "IsOficial",
                table: "PersonOfInterests");
        }
    }
}
