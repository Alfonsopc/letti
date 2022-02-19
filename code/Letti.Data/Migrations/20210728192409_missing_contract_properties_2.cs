using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class missing_contract_properties_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Licitation",
                table: "Contracts",
                newName: "Description");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Bidding",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Contracts",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bidding",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Contracts",
                newName: "Licitation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
