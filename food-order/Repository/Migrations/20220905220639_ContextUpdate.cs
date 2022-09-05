using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class ContextUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodCartEntry",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCartEntry", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodCartItem",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    foodid = table.Column<int>(type: "INTEGER", nullable: true),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodCartEntryid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCartItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_FoodCartItem_FoodCartEntry_FoodCartEntryid",
                        column: x => x.FoodCartEntryid,
                        principalTable: "FoodCartEntry",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FoodCartItem_foods_foodid",
                        column: x => x.foodid,
                        principalTable: "foods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(type: "INTEGER", nullable: true),
                    foodOrderEntryid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orders_FoodCartEntry_foodOrderEntryid",
                        column: x => x.foodOrderEntryid,
                        principalTable: "FoodCartEntry",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shoppingCarts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(type: "INTEGER", nullable: true),
                    foodCartEntryid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCarts", x => x.id);
                    table.ForeignKey(
                        name: "FK_shoppingCarts_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_shoppingCarts_FoodCartEntry_foodCartEntryid",
                        column: x => x.foodCartEntryid,
                        principalTable: "FoodCartEntry",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodCartItem_FoodCartEntryid",
                table: "FoodCartItem",
                column: "FoodCartEntryid");

            migrationBuilder.CreateIndex(
                name: "IX_FoodCartItem_foodid",
                table: "FoodCartItem",
                column: "foodid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_foodOrderEntryid",
                table: "orders",
                column: "foodOrderEntryid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_userId",
                table: "orders",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCarts_foodCartEntryid",
                table: "shoppingCarts",
                column: "foodCartEntryid");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCarts_userId",
                table: "shoppingCarts",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodCartItem");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "shoppingCarts");

            migrationBuilder.DropTable(
                name: "FoodCartEntry");
        }
    }
}
