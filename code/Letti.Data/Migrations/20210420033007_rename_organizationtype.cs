using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class rename_organizationtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonCompanyRelationships_CompanyRelationshipTypes_OrganizationRelationshipTypeId",
                table: "PersonCompanyRelationships");

            migrationBuilder.DropIndex(
                name: "IX_PersonCompanyRelationships_OrganizationRelationshipTypeId",
                table: "PersonCompanyRelationships");

            migrationBuilder.DropColumn(
                name: "OrganizationRelationshipTypeId",
                table: "PersonCompanyRelationships");

            migrationBuilder.AddColumn<int>(
                name: "ContractorRelationshipTypeId",
                table: "PersonCompanyRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanyRelationships_ContractorRelationshipTypeId",
                table: "PersonCompanyRelationships",
                column: "ContractorRelationshipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCompanyRelationships_CompanyRelationshipTypes_ContractorRelationshipTypeId",
                table: "PersonCompanyRelationships",
                column: "ContractorRelationshipTypeId",
                principalTable: "CompanyRelationshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonCompanyRelationships_CompanyRelationshipTypes_ContractorRelationshipTypeId",
                table: "PersonCompanyRelationships");

            migrationBuilder.DropIndex(
                name: "IX_PersonCompanyRelationships_ContractorRelationshipTypeId",
                table: "PersonCompanyRelationships");

            migrationBuilder.DropColumn(
                name: "ContractorRelationshipTypeId",
                table: "PersonCompanyRelationships");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationRelationshipTypeId",
                table: "PersonCompanyRelationships",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanyRelationships_OrganizationRelationshipTypeId",
                table: "PersonCompanyRelationships",
                column: "OrganizationRelationshipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCompanyRelationships_CompanyRelationshipTypes_OrganizationRelationshipTypeId",
                table: "PersonCompanyRelationships",
                column: "OrganizationRelationshipTypeId",
                principalTable: "CompanyRelationshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
