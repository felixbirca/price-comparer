using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceComparer.Infrastucture.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferOfferType_Services_ServicesId",
                table: "OfferOfferType");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferOfferType_ServiceTypes_OfferTypesId",
                table: "OfferOfferType");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Services_ServiceId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTypes",
                table: "ServiceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "ServiceTypes",
                newName: "OfferTypes");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Offers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferTypes",
                table: "OfferTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferOfferType_Offers_ServicesId",
                table: "OfferOfferType",
                column: "ServicesId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferOfferType_OfferTypes_OfferTypesId",
                table: "OfferOfferType",
                column: "OfferTypesId",
                principalTable: "OfferTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Offers_ServiceId",
                table: "Reviews",
                column: "ServiceId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferOfferType_Offers_ServicesId",
                table: "OfferOfferType");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferOfferType_OfferTypes_OfferTypesId",
                table: "OfferOfferType");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Offers_ServiceId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferTypes",
                table: "OfferTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "OfferTypes",
                newName: "ServiceTypes");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Services");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTypes",
                table: "ServiceTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferOfferType_Services_ServicesId",
                table: "OfferOfferType",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferOfferType_ServiceTypes_OfferTypesId",
                table: "OfferOfferType",
                column: "OfferTypesId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Services_ServiceId",
                table: "Reviews",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
