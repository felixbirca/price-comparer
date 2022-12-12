using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceComparer.Infrastucture.Migrations
{
    public partial class RenameEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceServiceType");

            migrationBuilder.CreateTable(
                name: "OfferOfferType",
                columns: table => new
                {
                    OfferTypesId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferOfferType", x => new { x.OfferTypesId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_OfferOfferType_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferOfferType_ServiceTypes_OfferTypesId",
                        column: x => x.OfferTypesId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferOfferType_ServicesId",
                table: "OfferOfferType",
                column: "ServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferOfferType");

            migrationBuilder.CreateTable(
                name: "ServiceServiceType",
                columns: table => new
                {
                    ServiceTypesId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceServiceType", x => new { x.ServiceTypesId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ServiceServiceType_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceServiceType_ServiceTypes_ServiceTypesId",
                        column: x => x.ServiceTypesId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceServiceType_ServicesId",
                table: "ServiceServiceType",
                column: "ServicesId");
        }
    }
}
