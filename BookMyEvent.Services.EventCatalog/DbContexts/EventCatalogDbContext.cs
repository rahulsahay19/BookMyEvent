using BookMyEvent.Services.EventCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookMyEvent.Services.EventCatalog.DbContexts
{
    public class EventCatalogDbContext : DbContext
    {
        public EventCatalogDbContext(DbContextOptions<EventCatalogDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<IntegrationEventLog> IntegrationEventLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var concertGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var musicalGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var playGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var conferenceGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = concertGuid,
                Name = "Concerts"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = musicalGuid,
                Name = "Musicals"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = playGuid,
                Name = "Plays"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = conferenceGuid,
                Name = "Conferences"
            });


            var venue1Guid = Guid.Parse("{bcd82a4c-881e-44fc-ba94-c857ab1ab071}");
            modelBuilder.Entity<Venue>().HasData(new Venue
            {
                VenueId = venue1Guid,
                Name = "Massey Hall",
                City = "Toronto",
                Country = "Canada"
            });

            var venue2Guid = Guid.Parse("{45929e17-9f3c-4dd4-8043-126c34733dc5}");
            modelBuilder.Entity<Venue>().HasData(new Venue
            {
                VenueId = venue2Guid,
                Name = "L'Olympia",
                City = "Montreal",
                Country = "Canada"
            });

            var venue3Guid = Guid.Parse("{2d8f80ca-616f-4928-bd09-a1849fec5a9a}");
            modelBuilder.Entity<Venue>().HasData(new Venue
            {
                VenueId = venue3Guid,
                Name = "Commodore Ballroom",
                City = "Vancouver",
                Country = "Canada"
            });

            DateTime eventDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0);
            DateTime evDt1 = eventDate.AddMonths(6);

            var ev1 = new Event
            {
                EventId = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
                Name = "John Egbert Live",
                Price = 65,
                Artist = "John Elbert",
                Date = evDt1,
                Description = "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.",
                ImageUrl = "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/banjo.jpg",
                CategoryId = concertGuid,
                VenueId = venue1Guid
            };

            modelBuilder.Entity<Event>().HasData(ev1);

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
                Name = "The State of Affairs: Michael Live!",
                Price = 85,
                Artist = "Michael Johnson",
                Date = eventDate.AddMonths(9),
                Description = "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?",
                ImageUrl = "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/michael.jpg",
                CategoryId = concertGuid,
                VenueId = venue1Guid
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{B419A7CA-3321-4F38-BE8E-4D7B6A529319}"),
                Name = "Clash of the DJs",
                Price = 85,
                Artist = "DJ 'The Mike'",
                Date = eventDate.AddMonths(4),
                Description = "DJs from all over the world will compete in this epic battle for eternal fame.",
                ImageUrl = "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/dj.jpg",
                CategoryId = concertGuid,
                VenueId = venue2Guid
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{62787623-4C52-43FE-B0C9-B7044FB5929B}"),
                Name = "Spanish guitar hits with Manuel",
                Price = 25,
                Artist = "Manuel Santinonisi",
                Date = eventDate.AddMonths(4),
                Description = "Get on the hype of Spanish Guitar concerts with Manuel.",
                ImageUrl = "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/guitar.jpg",
                CategoryId = concertGuid,
                VenueId = venue2Guid
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{1BABD057-E980-4CB3-9CD2-7FDD9E525668}"),
                Name = "Techorama 2021",
                Price = 400,
                Artist = "Many",
                Date = eventDate.AddMonths(10),
                Description = "The best tech conference in the world",
                ImageUrl = "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/conf.jpg",
                CategoryId = conferenceGuid,
                VenueId = venue3Guid
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{ADC42C09-08C1-4D2C-9F96-2D15BB1AF299}"),
                Name = "To the Moon and Back",
                Price = 135,
                Artist = "Nick Sailor",
                Date = eventDate.AddMonths(8),
                Description = "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.",
                ImageUrl = "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/musical.jpg",
                CategoryId = musicalGuid,
                VenueId = venue3Guid
            });
        }
    }
}
