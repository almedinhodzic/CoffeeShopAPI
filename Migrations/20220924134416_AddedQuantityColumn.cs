using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShopAPI.Migrations
{
    public partial class AddedQuantityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0108a4d5-9700-4ffb-8f4b-3f5b2fa0df2e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bb71d5a-0ed2-42f6-9447-edc712af1c0c");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "OrderProducts",
                newName: "Quantity");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83e4052e-9eb3-4928-a971-c1e905eab27b", "e15605d4-4d44-4f16-bfad-d6f4ab310335", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e607b999-1eb1-42fc-a7ad-9e74db3aed65", "2512e63b-a0a8-4a42-8a4a-88c14ea5ebfa", "user", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83e4052e-9eb3-4928-a971-c1e905eab27b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e607b999-1eb1-42fc-a7ad-9e74db3aed65");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderProducts",
                newName: "Total");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0108a4d5-9700-4ffb-8f4b-3f5b2fa0df2e", "a65589b9-435a-4884-8588-92ec6b0552e5", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8bb71d5a-0ed2-42f6-9447-edc712af1c0c", "cf7bfb82-2348-48ae-be86-8f2d154abe7d", "user", "USER" });
        }
    }
}
