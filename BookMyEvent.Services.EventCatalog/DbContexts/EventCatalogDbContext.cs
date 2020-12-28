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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var concertGuid = Guid.Parse("{85b8eb31-4109-4864-abdd-f855b7975b4b}");
            var musicGuid = Guid.Parse("{b23b9719-ea27-4eb3-bb39-7092c11a44bc}");
            var dramaGuid = Guid.Parse("{c7f58e81-f69e-4991-b8a6-ff5f80a11435}");
            var movieGuid = Guid.Parse("5aed2b89-b2f8-4b13-979f-66500fa968cb");

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = concertGuid,
                Name = "Concerts"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = musicGuid,
                Name = "Musicals"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = dramaGuid,
                Name = "Plays"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = movieGuid,
                Name = "Movies"
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{CFB88E29-4744-48C0-94FA-B25B92DEA317}"),
                Name = "John Egbert Live",
                Price = 65,
                Artist = "John Egbert",
                Date = DateTime.Now.AddMonths(6),
                Description = "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.",
                ImageUrl = "/img/banjo.jpg",
                CategoryId = concertGuid
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{CFB88E29-4744-48C0-94FA-B25B92DEA319}"),
                Name = "The State of Affairs: Michael Live!",
                Price = 85,
                Artist = "Michael Johnson",
                Date = DateTime.Now.AddMonths(9),
                Description = "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?",
                ImageUrl = "/img/michael.jpg",
                CategoryId = concertGuid
            });


            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{CFB88E29-4744-48C0-94FA-B25B92DEA318}"),
                Name = "To the Moon and Back",
                Price = 135,
                Artist = "Nick Sailor",
                Date = DateTime.Now.AddMonths(8),
                Description = "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.",
                ImageUrl = "/img/musical.jpg",
                CategoryId = musicGuid
            });
        }
    }

}

