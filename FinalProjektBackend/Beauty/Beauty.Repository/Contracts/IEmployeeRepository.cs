using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeAsync(int employeeId);

        Task<Employee> GetEmployeeByUserIdAsync(int employeeId);

        Task CreateEmployeeAsync(Employee employee);

        void DeleteEmployee(Employee employee);

        Task SaveAsync();
    }
}
