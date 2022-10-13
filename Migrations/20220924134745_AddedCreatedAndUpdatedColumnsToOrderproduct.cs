using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShopAPI.Migrations
{
    public partial class AddedCreatedAndUpdatedColumnsToOrderproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83e4052e-9eb3-4928-a971-c1e905eab27b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e607b999-1eb1-42fc-a7ad-9e74db3aed65");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OrderProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "OrderProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fbde2f4-eaf2-420f-bbe4-ea37c8d30314", "5293ff2b-b4ec-4b9b-bb35-74851687113e", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83f6fd19-f718-4ee3-af29-edb9be8a835f", "8bf50b78-daea-4c7f-b0b6-8fb5dd0c38c6", "user", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fbde2f4-eaf2-420f-bbe4-ea37c8d30314");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83f6fd19-f718-4ee3-af29-edb9be8a835f");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "OrderProducts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83e4052e-9eb3-4928-a971-c1e905eab27b", "e15605d4-4d44-4f16-bfad-d6f4ab310335", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e607b999-1eb1-42fc-a7ad-9e74db3aed65", "2512e63b-a0a8-4a42-8a4a-88c14ea5ebfa", "user", "USER" });
        }
    }
}
