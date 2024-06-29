using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _context.Employees!.AddAsync(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees!.Remove(employee);
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return await _context.Employees
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id.Equals(employeeId));
        }

        public async Task<Employee> GetEmployeeByUserIdAsync(int employeeId)
        {
            return await _context.Employees
                .SingleOrDefaultAsync(x => x.UserId.Equals(employeeId));
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
