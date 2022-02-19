using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class contract_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizedById",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Licitation",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Licitation",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "AuthorizedById",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
