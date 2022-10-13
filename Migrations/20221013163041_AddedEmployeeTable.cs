using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShopAPI.Migrations
{
    public partial class AddedEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fbde2f4-eaf2-420f-bbe4-ea37c8d30314");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83f6fd19-f718-4ee3-af29-edb9be8a835f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "390f26ed-6128-4980-bd18-b165bbfec0fd", "cc7bf4c5-3d91-4d7d-b6a0-a848dbdd9d28", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da64b1f5-c9f7-4d6e-9d82-e7bc03fbaedc", "67f00c51-1b69-4282-931b-72f813331755", "user", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "390f26ed-6128-4980-bd18-b165bbfec0fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da64b1f5-c9f7-4d6e-9d82-e7bc03fbaedc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fbde2f4-eaf2-420f-bbe4-ea37c8d30314", "5293ff2b-b4ec-4b9b-bb35-74851687113e", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83f6fd19-f718-4ee3-af29-edb9be8a835f", "8bf50b78-daea-4c7f-b0b6-8fb5dd0c38c6", "user", "USER" });
        }
    }
}
