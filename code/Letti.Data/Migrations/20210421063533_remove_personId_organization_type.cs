using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class remove_personId_organization_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonOrganizationRelationships_OrganizationRelationshipTypes_OrganizationRelationshipTypeId",
                table: "PersonOrganizationRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonOrganizationRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonOrganizationRelationships");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonOrganizationRelationships");

            migrationBuilder.AlterColumn<int>(
                name: "PersonOfInterestId",
                table: "PersonOrganizationRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationRelationshipTypeId",
                table: "PersonOrganizationRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonOrganizationRelationships_OrganizationRelationshipTypes_OrganizationRelationshipTypeId",
                table: "PersonOrganizationRelationships",
                column: "OrganizationRelationshipTypeId",
                principalTable: "OrganizationRelationshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonOrganizationRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonOrganizationRelationships",
                column: "PersonOfInterestId",
                principalTable: "PersonOfInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonOrganizationRelationships_OrganizationRelationshipTypes_OrganizationRelationshipTypeId",
                table: "PersonOrganizationRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonOrganizationRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonOrganizationRelationships");

            migrationBuilder.AlterColumn<int>(
                name: "PersonOfInterestId",
                table: "PersonOrganizationRelationships",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationRelationshipTypeId",
                table: "PersonOrganizationRelationships",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "PersonOrganizationRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonOrganizationRelationships_OrganizationRelationshipTypes_OrganizationRelationshipTypeId",
                table: "PersonOrganizationRelationships",
                column: "OrganizationRelationshipTypeId",
                principalTable: "OrganizationRelationshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonOrganizationRelationships_PersonOfInterests_PersonOfInterestId",
                table: "PersonOrganizationRelationships",
                column: "PersonOfInterestId",
                principalTable: "PersonOfInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
