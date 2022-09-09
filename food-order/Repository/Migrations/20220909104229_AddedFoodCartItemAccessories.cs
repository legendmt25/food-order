using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class AddedFoodCartItemAccessories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodCartItemid",
                table: "FoodAccessory",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodAccessory_FoodCartItemid",
                table: "FoodAccessory",
                column: "FoodCartItemid");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodAccessory_FoodCartItem_FoodCartItemid",
                table: "FoodAccessory",
                column: "FoodCartItemid",
                principalTable: "FoodCartItem",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodAccessory_FoodCartItem_FoodCartItemid",
                table: "FoodAccessory");

            migrationBuilder.DropIndex(
                name: "IX_FoodAccessory_FoodCartItemid",
                table: "FoodAccessory");

            migrationBuilder.DropColumn(
                name: "FoodCartItemid",
                table: "FoodAccessory");
        }
    }
}
