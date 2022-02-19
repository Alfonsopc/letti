using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class relationshipt_type_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRelationShips_PersonalRelationshipTypes_PersonalRelationshipTypeId",
                table: "PersonalRelationShips");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalRelationshipTypeId",
                table: "PersonalRelationShips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalRelationShips_PersonalRelationshipTypes_PersonalRelationshipTypeId",
                table: "PersonalRelationShips",
                column: "PersonalRelationshipTypeId",
                principalTable: "PersonalRelationshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRelationShips_PersonalRelationshipTypes_PersonalRelationshipTypeId",
                table: "PersonalRelationShips");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalRelationshipTypeId",
                table: "PersonalRelationShips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalRelationShips_PersonalRelationshipTypes_PersonalRelationshipTypeId",
                table: "PersonalRelationShips",
                column: "PersonalRelationshipTypeId",
                principalTable: "PersonalRelationshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
