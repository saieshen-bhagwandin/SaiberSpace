using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class seedingfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EditionProduct",
                keyColumns: new[] { "EditionsId", "ProductsId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.InsertData(
                table: "EditionProduct",
                columns: new[] { "EditionsId", "ProductsId" },
                values: new object[] { 7, 7 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EditionProduct",
                keyColumns: new[] { "EditionsId", "ProductsId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.InsertData(
                table: "EditionProduct",
                columns: new[] { "EditionsId", "ProductsId" },
                values: new object[] { 4, 7 });
        }
    }
}
