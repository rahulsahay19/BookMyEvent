using BookMyEvent.Services.Ordering.DbContexts;
using BookMyEvent.Services.Ordering.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Ordering.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContextOptions<OrderDbContext> dbContextOptions;

        public CustomerRepository(DbContextOptions<OrderDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<Customer> GetCustomerById(Guid customerId)
        {
            using (var _orderDbContext = new OrderDbContext(dbContextOptions))
            {
                var customer = await _orderDbContext.Customers.Where(c => c.CustomerId == customerId).FirstOrDefaultAsync();
                if (customer == null)
                {
                    // TODO: get customer from the Customer management microservice and store in local database

                }
                return customer;
            }
        }

        public async Task AddCustomer(Customer customer)
        {
            await using (var _orderDbContext = new OrderDbContext(dbContextOptions))
            {
                await _orderDbContext.Customers.AddAsync(customer);
                await _orderDbContext.SaveChangesAsync();
            }
        }
    }
}
