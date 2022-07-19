using Microsoft.EntityFrameworkCore.Migrations;

namespace FRONTTOBACK.Migrations
{
    public partial class salesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "SalesProducts");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "SalesProducts",
                newName: "Price");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "SalesProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SalesProducts",
                newName: "price");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "SalesProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SalesId",
                table: "SalesProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
