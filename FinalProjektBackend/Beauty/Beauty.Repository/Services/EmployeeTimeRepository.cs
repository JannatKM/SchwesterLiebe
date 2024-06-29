using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class EmployeeTimeRepository : IEmployeeTimeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateEmployeeTimeAsync(EmployeeTime employeeTime)
        {
            await _context.EmployeeTimes!.AddAsync(employeeTime);
        }


        public void DeleteEmployeeTime(EmployeeTime employeeTime)
        {
            _context.EmployeeTimes!.Remove(employeeTime);
        }


        public async Task<EmployeeTime?> GetEmployeeTimeByDetailsAsync(int employeeId, string date, int time)
        {
            return await _context.EmployeeTimes!
                .Include(et => et.Employee)
                .ThenInclude(emp => emp!.User)
                .FirstOrDefaultAsync(x => 
                            x.EmployeeId.Equals(employeeId) && 
                            x.Date!.Equals(date) &&
                            x.Time.Equals(time));
        }


        public async Task<IEnumerable<EmployeeTime>> GetEmployeeTimesAsync()
        {
            return await _context.EmployeeTimes!
                .Include(et => et.Employee)
                .ThenInclude(emp => emp!.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeTime>> GetEmployeeTimesByDateAsync(string date)
        {
            return await _context.EmployeeTimes!
                .Where(ec => ec.Date!.Equals(date))
                .Include(ec => ec.Employee)
                .ThenInclude(emp => emp!.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeTime>> GetEmployeeTimesByDateAsync(int id, string date)
        {
            return await _context.EmployeeTimes!
                .Where(ec => ec.EmployeeId.Equals(id) && 
                                          ec.Date!.Equals(date))
                .Include(ec => ec.Employee)
                .ThenInclude(emp => emp!.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeTime>> GetEmployeeTimesByEmployeeIdAsync(int id)
        {
            return await _context.EmployeeTimes!
                .Where(ec => ec.EmployeeId.Equals(id))
                .Include(ec => ec.Employee)
                .ThenInclude(emp => emp!.User)
                .ToListAsync();
        }

        public async Task<EmployeeTime> GetEmployeeTimeByIdAsync(int id)
        {
            return await _context.EmployeeTimes!
                .Include(ec => ec.Employee)
                .ThenInclude(emp => emp!.User)
                .FirstOrDefaultAsync(ec => ec.Id.Equals(id));
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
