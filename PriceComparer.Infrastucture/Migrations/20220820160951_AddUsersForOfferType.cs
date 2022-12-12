using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceComparer.Infrastucture.Migrations
{
    public partial class AddUsersForOfferType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OfferTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OfferTypes");
        }
    }
}
