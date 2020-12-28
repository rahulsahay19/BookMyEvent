using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMyEvent.Services.EventCatalog.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Artist = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("85b8eb31-4109-4864-abdd-f855b7975b4b"), "Concerts" },
                    { new Guid("b23b9719-ea27-4eb3-bb39-7092c11a44bc"), "Musicals" },
                    { new Guid("c7f58e81-f69e-4991-b8a6-ff5f80a11435"), "Plays" },
                    { new Guid("5aed2b89-b2f8-4b13-979f-66500fa968cb"), "Movies" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new Guid("3e1ba46d-d744-48cc-b07a-9ef940f000e0"), "John Egbert", new Guid("85b8eb31-4109-4864-abdd-f855b7975b4b"), new DateTime(2021, 6, 28, 19, 10, 30, 163, DateTimeKind.Local).AddTicks(4688), "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.", "/img/banjo.jpg", "John Egbert Live", 65 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new Guid("dd059a8f-05eb-4427-a50f-2aba0d13c1f7"), "Michael Johnson", new Guid("85b8eb31-4109-4864-abdd-f855b7975b4b"), new DateTime(2021, 9, 28, 19, 10, 30, 165, DateTimeKind.Local).AddTicks(160), "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?", "/img/michael.jpg", "The State of Affairs: Michael Live!", 85 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new Guid("47a7881d-84f0-47c1-bf87-fc306d537b99"), "Nick Sailor", new Guid("b23b9719-ea27-4eb3-bb39-7092c11a44bc"), new DateTime(2021, 8, 28, 19, 10, 30, 165, DateTimeKind.Local).AddTicks(275), "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.", "/img/musical.jpg", "To the Moon and Back", 135 });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
