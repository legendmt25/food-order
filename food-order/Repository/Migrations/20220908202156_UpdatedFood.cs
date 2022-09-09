using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class UpdatedFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "imageid",
                table: "foods",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "foods",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageData",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data = table.Column<byte[]>(type: "BLOB", nullable: true),
                    contentType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageData", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_foods_imageid",
                table: "foods",
                column: "imageid");

            migrationBuilder.AddForeignKey(
                name: "FK_foods_ImageData_imageid",
                table: "foods",
                column: "imageid",
                principalTable: "ImageData",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_foods_ImageData_imageid",
                table: "foods");

            migrationBuilder.DropTable(
                name: "ImageData");

            migrationBuilder.DropIndex(
                name: "IX_foods_imageid",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "imageid",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "name",
                table: "foods");
        }
    }
}
