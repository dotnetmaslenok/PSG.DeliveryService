using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSG.DeliveryService.Infrastructure.Migrations
{
    public partial class AddEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couriers_PassportCredentials_PassportId",
                table: "Couriers");

            migrationBuilder.DropTable(
                name: "PassportCredentials");

            migrationBuilder.DropIndex(
                name: "IX_Couriers_PassportId",
                table: "Couriers");

            migrationBuilder.DropColumn(
                name: "PassportId",
                table: "Couriers");

            migrationBuilder.AlterColumn<int>(
                name: "OrderWeight",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,3)",
                oldPrecision: 8,
                oldScale: 3);

            migrationBuilder.AddColumn<int>(
                name: "OrderType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderWeight",
                table: "Orders",
                type: "decimal(8,3)",
                precision: 8,
                scale: 3,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PassportId",
                table: "Couriers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PassportCredentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassportCredentials", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_PassportId",
                table: "Couriers",
                column: "PassportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Couriers_PassportCredentials_PassportId",
                table: "Couriers",
                column: "PassportId",
                principalTable: "PassportCredentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
