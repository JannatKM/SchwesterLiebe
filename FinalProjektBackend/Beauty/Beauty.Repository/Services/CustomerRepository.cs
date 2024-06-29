using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<Customer> GetCustomerAsync(int customerId)
        {
            return await _context.Customers
                .SingleOrDefaultAsync(x => x.Id.Equals(customerId));
        }

        public async Task<Customer> GetCustomerByUserIdAsync(int customerId)
        {
            return await _context.Customers
                .SingleOrDefaultAsync(x => x.UserId.Equals(customerId));
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
