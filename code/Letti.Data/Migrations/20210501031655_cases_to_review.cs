using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class cases_to_review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseType = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SuspiciousRelationship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationPoiId = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorPoiId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseToReviewId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuspiciousRelationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuspiciousRelationship_Cases_CaseToReviewId",
                        column: x => x.CaseToReviewId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuspiciousRelationship_PersonOfInterests_ContractorPoiId",
                        column: x => x.ContractorPoiId,
                        principalTable: "PersonOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuspiciousRelationship_PersonOfInterests_OrganizationPoiId",
                        column: x => x.OrganizationPoiId,
                        principalTable: "PersonOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ContractorId",
                table: "Cases",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_OrganizationId",
                table: "Cases",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SuspiciousRelationship_CaseToReviewId",
                table: "SuspiciousRelationship",
                column: "CaseToReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_SuspiciousRelationship_ContractorPoiId",
                table: "SuspiciousRelationship",
                column: "ContractorPoiId");

            migrationBuilder.CreateIndex(
                name: "IX_SuspiciousRelationship_OrganizationPoiId",
                table: "SuspiciousRelationship",
                column: "OrganizationPoiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuspiciousRelationship");

            migrationBuilder.DropTable(
                name: "Cases");
        }
    }
}
