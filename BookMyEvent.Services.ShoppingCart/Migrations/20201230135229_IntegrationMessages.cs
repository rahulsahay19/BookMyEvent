using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMyEvent.Services.ShoppingCart.Migrations
{
    public partial class IntegrationMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "Baskets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BasketChangeEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTimeOffset>(nullable: false),
                    BasketChangeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketChangeEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketChangeEvents");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Baskets");
        }
    }
}
