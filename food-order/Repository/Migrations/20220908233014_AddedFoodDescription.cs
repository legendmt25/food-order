using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class AddedFoodDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "foods",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "size",
                table: "FoodCartItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "size",
                table: "FoodCartItem");
        }
    }
}
