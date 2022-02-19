using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class missing_properties_for_scrapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CadastralCode",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certificate",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Colony",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerType",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Record",
                table: "Contractors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CadastralCode",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Colony",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OwnerType",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Record",
                table: "Contractors");
        }
    }
}
