using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Palfinger.ProductManual.Infrastructure.Data.Migrations
{
    public partial class addManualWithManyProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManualId",
                table: "Product",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Manual",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manual", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ManualId",
                table: "Product",
                column: "ManualId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Manual_ManualId",
                table: "Product",
                column: "ManualId",
                principalTable: "Manual",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Manual_ManualId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Manual");

            migrationBuilder.DropIndex(
                name: "IX_Product_ManualId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ManualId",
                table: "Product");
        }
    }
}
