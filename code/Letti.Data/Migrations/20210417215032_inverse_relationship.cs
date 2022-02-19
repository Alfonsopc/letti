using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class inverse_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InverseRelationshipType",
                table: "PersonalRelationshipTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InverseRelationshipType",
                table: "PersonalRelationshipTypes");
        }
    }
}
