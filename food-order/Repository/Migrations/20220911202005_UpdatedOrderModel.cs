using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class UpdatedOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "addressid",
                table: "orders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_addressid",
                table: "orders",
                column: "addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_userAddresses_addressid",
                table: "orders",
                column: "addressid",
                principalTable: "userAddresses",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_userAddresses_addressid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_addressid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "addressid",
                table: "orders");
        }
    }
}
