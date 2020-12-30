using BookMyEvent.Services.ShoppingCart.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyEvent.Services.ShoppingCart.DbContexts
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options): base(options)
        {

        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketLine> BasketLines { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<BasketChangeEvent> BasketChangeEvents { get; set; }
    }
}
