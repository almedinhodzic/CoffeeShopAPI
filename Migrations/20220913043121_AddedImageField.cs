using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShopAPI.Migrations
{
    public partial class AddedImageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Products");
        }
    }
}
