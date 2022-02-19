using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class organization_contractor_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Contracts",
                newName: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractorId",
                table: "Contracts",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OrganizationId",
                table: "Contracts",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Contractors_ContractorId",
                table: "Contracts",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Organizations_OrganizationId",
                table: "Contracts",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Contractors_ContractorId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Organizations_OrganizationId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractorId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_OrganizationId",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "ContractorId",
                table: "Contracts",
                newName: "CompanyId");
        }
    }
}
