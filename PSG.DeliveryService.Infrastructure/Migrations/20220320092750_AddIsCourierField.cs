using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSG.DeliveryService.Infrastructure.Migrations
{
    public partial class AddIsCourierField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCourier",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCourier",
                table: "AspNetUsers");
        }
    }
}
