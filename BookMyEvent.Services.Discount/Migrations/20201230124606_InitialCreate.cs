using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMyEvent.Services.Discount.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    AlreadyUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "AlreadyUsed", "Amount", "Code" },
                values: new object[] { new Guid("64709060-67ae-4541-89ec-625c72fe743d"), false, 10, "BeNice" });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "AlreadyUsed", "Amount", "Code" },
                values: new object[] { new Guid("505421e6-dae5-47cf-88f6-6f84c5d32601"), false, 20, "Awesome" });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "AlreadyUsed", "Amount", "Code" },
                values: new object[] { new Guid("fb2d6cdb-8a10-4412-b649-c0441db4c47f"), false, 100, "AlmostFree" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
