using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class remove_personId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonCompanyRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonCompanyRelationships");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonCompanyRelationships");

            migrationBuilder.AlterColumn<int>(
                name: "PersonOfInterestId",
                table: "PersonCompanyRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCompanyRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonCompanyRelationships",
                column: "PersonOfInterestId",
                principalTable: "PersonOfInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonCompanyRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonCompanyRelationships");

            migrationBuilder.AlterColumn<int>(
                name: "PersonOfInterestId",
                table: "PersonCompanyRelationships",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "PersonCompanyRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCompanyRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonCompanyRelationships",
                column: "PersonOfInterestId",
                principalTable: "PersonOfInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
