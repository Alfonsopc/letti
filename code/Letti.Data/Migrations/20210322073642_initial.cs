using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComercialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiscalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationshipType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Concept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationRelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationshipType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationType = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalRelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationshipType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonOfInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalRelationShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainPersonId = table.Column<int>(type: "int", nullable: false),
                    SecondaryPersonId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonalRelationshipTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRelationShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalRelationShips_PersonalRelationshipTypes_PersonalRelationshipTypeId",
                        column: x => x.PersonalRelationshipTypeId,
                        principalTable: "PersonalRelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalRelationShips_PersonOfInterests_MainPersonId",
                        column: x => x.MainPersonId,
                        principalTable: "PersonOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonalRelationShips_PersonOfInterests_SecondaryPersonId",
                        column: x => x.SecondaryPersonId,
                        principalTable: "PersonOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonCompanyRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PersonOfInterestId = table.Column<int>(type: "int", nullable: true),
                    OrganizationRelationshipTypeId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCompanyRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCompanyRelationships_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCompanyRelationships_CompanyRelationshipTypes_OrganizationRelationshipTypeId",
                        column: x => x.OrganizationRelationshipTypeId,
                        principalTable: "CompanyRelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCompanyRelationships_PersonOfInterests_PersonOfInterestId",
                        column: x => x.PersonOfInterestId,
                        principalTable: "PersonOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonOrganizationRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PersonOfInterestId = table.Column<int>(type: "int", nullable: true),
                    OrganizationRelationshipTypeId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonOrganizationRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonOrganizationRelationships_OrganizationRelationshipTypes_OrganizationRelationshipTypeId",
                        column: x => x.OrganizationRelationshipTypeId,
                        principalTable: "OrganizationRelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonOrganizationRelationships_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonOrganizationRelationships_PersonOfInterests_PersonOfInterestId",
                        column: x => x.PersonOfInterestId,
                        principalTable: "PersonOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRelationShips_MainPersonId",
                table: "PersonalRelationShips",
                column: "MainPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRelationShips_PersonalRelationshipTypeId",
                table: "PersonalRelationShips",
                column: "PersonalRelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRelationShips_SecondaryPersonId",
                table: "PersonalRelationShips",
                column: "SecondaryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanyRelationships_CompanyId",
                table: "PersonCompanyRelationships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanyRelationships_OrganizationRelationshipTypeId",
                table: "PersonCompanyRelationships",
                column: "OrganizationRelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanyRelationships_PersonOfInterestId",
                table: "PersonCompanyRelationships",
                column: "PersonOfInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOrganizationRelationships_OrganizationId",
                table: "PersonOrganizationRelationships",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOrganizationRelationships_OrganizationRelationshipTypeId",
                table: "PersonOrganizationRelationships",
                column: "OrganizationRelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOrganizationRelationships_PersonOfInterestId",
                table: "PersonOrganizationRelationships",
                column: "PersonOfInterestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "PersonalRelationShips");

            migrationBuilder.DropTable(
                name: "PersonCompanyRelationships");

            migrationBuilder.DropTable(
                name: "PersonOrganizationRelationships");

            migrationBuilder.DropTable(
                name: "PersonalRelationshipTypes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyRelationshipTypes");

            migrationBuilder.DropTable(
                name: "OrganizationRelationshipTypes");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "PersonOfInterests");
        }
    }
}
