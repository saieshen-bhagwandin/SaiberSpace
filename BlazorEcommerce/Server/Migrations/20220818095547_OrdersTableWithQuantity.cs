using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class OrdersTableWithQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");
        }
    }
}
