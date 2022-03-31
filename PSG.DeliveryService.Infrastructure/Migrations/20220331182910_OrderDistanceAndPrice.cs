using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSG.DeliveryService.Infrastructure.Migrations
{
    public partial class OrderDistanceAndPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "ProductPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryPrice",
                table: "Orders",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
