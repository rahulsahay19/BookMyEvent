using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMyEvent.Services.EventCatalog.Migrations
{
    public partial class AddedVenueDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("5aed2b89-b2f8-4b13-979f-66500fa968cb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("c7f58e81-f69e-4991-b8a6-ff5f80a11435"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("3e1ba46d-d744-48cc-b07a-9ef940f000e0"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("47a7881d-84f0-47c1-bf87-fc306d537b99"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("dd059a8f-05eb-4427-a50f-2aba0d13c1f7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("85b8eb31-4109-4864-abdd-f855b7975b4b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("b23b9719-ea27-4eb3-bb39-7092c11a44bc"));

            migrationBuilder.AddColumn<Guid>(
                name: "VenueId",
                table: "Events",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "IntegrationEventLogs",
                columns: table => new
                {
                    IntegrationEventLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntegrationEventType = table.Column<string>(nullable: true),
                    ServiceBusTopicName = table.Column<string>(nullable: true),
                    IntegrationEventBody = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLogs", x => x.IntegrationEventLogId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Concerts" },
                    { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Musicals" },
                    { new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), "Plays" },
                    { new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), "Conferences" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "City", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("bcd82a4c-881e-44fc-ba94-c857ab1ab071"), "Toronto", "Canada", "Massey Hall" },
                    { new Guid("45929e17-9f3c-4dd4-8043-126c34733dc5"), "Montreal", "Canada", "L'Olympia" },
                    { new Guid("2d8f80ca-616f-4928-bd09-a1849fec5a9a"), "Vancouver", "Canada", "Commodore Ballroom" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price", "VenueId" },
                values: new object[,]
                {
                    { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), "John Elbert", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 7, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/banjo.jpg", "John Egbert Live", 65, new Guid("bcd82a4c-881e-44fc-ba94-c857ab1ab071") },
                    { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), "Michael Johnson", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 10, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/michael.jpg", "The State of Affairs: Michael Live!", 85, new Guid("bcd82a4c-881e-44fc-ba94-c857ab1ab071") },
                    { new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"), "DJ 'The Mike'", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 5, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "DJs from all over the world will compete in this epic battle for eternal fame.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/dj.jpg", "Clash of the DJs", 85, new Guid("45929e17-9f3c-4dd4-8043-126c34733dc5") },
                    { new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"), "Manuel Santinonisi", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 5, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "Get on the hype of Spanish Guitar concerts with Manuel.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/guitar.jpg", "Spanish guitar hits with Manuel", 25, new Guid("45929e17-9f3c-4dd4-8043-126c34733dc5") },
                    { new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"), "Many", new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), new DateTime(2021, 11, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "The best tech conference in the world", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/conf.jpg", "Techorama 2021", 400, new Guid("2d8f80ca-616f-4928-bd09-a1849fec5a9a") },
                    { new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"), "Nick Sailor", new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), new DateTime(2021, 9, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/musical.jpg", "To the Moon and Back", 135, new Guid("2d8f80ca-616f-4928-bd09-a1849fec5a9a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "IntegrationEventLogs");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Events_VenueId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"));

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Events");

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
                values: new object[] { new Guid("3e1ba46d-d744-48cc-b07a-9ef940f000e0"), "John Egbert", new Guid("85b8eb31-4109-4864-abdd-f855b7975b4b"), new DateTime(2021, 7, 3, 0, 25, 56, 94, DateTimeKind.Local).AddTicks(1726), "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.", "/img/banjo.jpg", "John Egbert Live", 65 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new Guid("dd059a8f-05eb-4427-a50f-2aba0d13c1f7"), "Michael Johnson", new Guid("85b8eb31-4109-4864-abdd-f855b7975b4b"), new DateTime(2021, 10, 3, 0, 25, 56, 95, DateTimeKind.Local).AddTicks(9090), "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?", "/img/michael.jpg", "The State of Affairs: Michael Live!", 85 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new Guid("47a7881d-84f0-47c1-bf87-fc306d537b99"), "Nick Sailor", new Guid("b23b9719-ea27-4eb3-bb39-7092c11a44bc"), new DateTime(2021, 9, 3, 0, 25, 56, 95, DateTimeKind.Local).AddTicks(9230), "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.", "/img/musical.jpg", "To the Moon and Back", 135 });
        }
    }
}
