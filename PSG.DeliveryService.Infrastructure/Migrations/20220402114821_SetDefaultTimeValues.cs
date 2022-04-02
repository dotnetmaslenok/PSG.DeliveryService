using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSG.DeliveryService.Infrastructure.Migrations
{
    public partial class SetDefaultTimeValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 2, 15, 18, 21, 685, DateTimeKind.Local).AddTicks(6065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserRegistrationTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 2, 14, 48, 21, 685, DateTimeKind.Local).AddTicks(4961),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 2, 15, 18, 21, 685, DateTimeKind.Local).AddTicks(6065));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserRegistrationTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 2, 14, 48, 21, 685, DateTimeKind.Local).AddTicks(4961));
        }
    }
}
