using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSG.DeliveryService.Infrastructure.Migrations
{
    public partial class ChangeDecimalRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 4, 2, 17, 17, 16, 719, DateTimeKind.Local).AddTicks(6262),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 2, 15, 18, 21, 685, DateTimeKind.Local).AddTicks(6065));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserRegistrationTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 2, 16, 47, 16, 719, DateTimeKind.Local).AddTicks(5332),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 2, 14, 48, 21, 685, DateTimeKind.Local).AddTicks(4961));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 2, 15, 18, 21, 685, DateTimeKind.Local).AddTicks(6065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 4, 2, 17, 17, 16, 719, DateTimeKind.Local).AddTicks(6262));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserRegistrationTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 2, 14, 48, 21, 685, DateTimeKind.Local).AddTicks(4961),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 2, 16, 47, 16, 719, DateTimeKind.Local).AddTicks(5332));
        }
    }
}
