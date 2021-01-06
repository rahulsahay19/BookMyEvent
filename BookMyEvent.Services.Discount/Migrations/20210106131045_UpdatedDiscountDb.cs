using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMyEvent.Services.Discount.Migrations
{
    public partial class UpdatedDiscountDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Amount", "UserId" },
                values: new object[] { new Guid("3f7dbbba-aecc-4f39-9a86-0a3f8a73396d"), 10, new Guid("e455a3df-7fa5-47e0-8435-179b300d531f") });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Amount", "UserId" },
                values: new object[] { new Guid("66e52ca9-6ab5-49a2-bed0-55eefabb73a9"), 20, new Guid("bbf594b0-3761-4a65-b04c-eec4836d9070") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
