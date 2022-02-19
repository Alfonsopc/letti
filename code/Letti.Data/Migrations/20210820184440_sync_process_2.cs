using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Letti.Data.Migrations
{
    public partial class sync_process_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractorScans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSigerScan = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorScans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorScans_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonScans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoiId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastPropertyRegistryScan = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonScans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonScans_PersonOfInterests_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorScans_ContractorId",
                table: "ContractorScans",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonScans_PersonId",
                table: "PersonScans",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorScans");

            migrationBuilder.DropTable(
                name: "PersonScans");
        }
    }
}
