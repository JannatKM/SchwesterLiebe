using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();

        Task<Customer> GetCustomerAsync(int customerId);

        Task<Customer> GetCustomerByUserIdAsync(int customerId);

        Task CreateCustomerAsync(Customer customer);

        void DeleteCustomer(Customer customer);

        Task SaveAsync();
    }
}
