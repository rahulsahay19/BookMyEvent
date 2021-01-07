using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMyEvent.Services.ShoppingCart.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "BasketLines",
                columns: table => new
                {
                    BasketLineId = table.Column<Guid>(nullable: false),
                    BasketId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    TicketAmount = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketLines", x => x.BasketLineId);
                    table.ForeignKey(
                        name: "FK_BasketLines_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketLines_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Date", "Name" },
                values: new object[,]
                {
                    { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), new DateTime(2021, 7, 7, 12, 55, 27, 480, DateTimeKind.Local).AddTicks(6910), "John Egbert Live" },
                    { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), new DateTime(2021, 10, 7, 12, 55, 27, 482, DateTimeKind.Local).AddTicks(1024), "The State of Affairs: Michael Live!" },
                    { new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"), new DateTime(2021, 5, 7, 12, 55, 27, 482, DateTimeKind.Local).AddTicks(1066), "Clash of the DJs" },
                    { new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"), new DateTime(2021, 5, 7, 12, 55, 27, 482, DateTimeKind.Local).AddTicks(1072), "Spanish guitar hits with Manuel" },
                    { new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"), new DateTime(2021, 11, 7, 12, 55, 27, 482, DateTimeKind.Local).AddTicks(1076), "Techorama 2021" },
                    { new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"), new DateTime(2021, 9, 7, 12, 55, 27, 482, DateTimeKind.Local).AddTicks(1081), "To the Moon and Back" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketLines_BasketId",
                table: "BasketLines",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketLines_EventId",
                table: "BasketLines",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketChangeEvents");

            migrationBuilder.DropTable(
                name: "BasketLines");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
