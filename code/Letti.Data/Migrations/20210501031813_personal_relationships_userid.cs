using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class personal_relationships_userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PersonalRelationShips",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PersonalRelationShips");
        }
    }
}
