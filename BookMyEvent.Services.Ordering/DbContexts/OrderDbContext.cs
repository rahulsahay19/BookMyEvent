using BookMyEvent.Services.Ordering.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyEvent.Services.Ordering.DbContexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
