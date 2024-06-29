using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class EmployeeCalendarRepository : IEmployeeCalendarRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeCalendarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateEmployeeCalendarAsync(EmployeeCalendar employeeCalendar)
        {
            await _context.EmployeeCalendars!.AddAsync(employeeCalendar);
        }


        public void DeleteEmployeeCalendar(EmployeeCalendar employeeCalendar)
        {
            _context.EmployeeCalendars!.Remove(employeeCalendar);
        }


        public async Task<EmployeeCalendar?> GetEmployeeCalendarAsync(int? id)
        {
            return await _context.EmployeeCalendars!
                .Include(ec => ec.Employee)
                .ThenInclude(emp => emp!.User)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }


        public async Task<IEnumerable<EmployeeCalendar>> GetDateByEmployeeIdAsync(int id)
        {
            return await _context.EmployeeCalendars!
                .Where(ec => ec.EmployeeId.Equals(id))
                .ToListAsync();
        }


        public async Task<IEnumerable<EmployeeCalendar>> GetEmployeeCalendarsAsync()
        {
            return await _context.EmployeeCalendars!
                .Include(ec => ec.Employee)
                .ThenInclude(emp => emp!.User)
                .ToListAsync();
        }


        public async Task<IEnumerable<EmployeeCalendar>> GetEmployeeCalendarsByEmployeeIdAsync(int id)
        {
            return await _context.EmployeeCalendars!
                .Where(ec => ec.EmployeeId.Equals(id))
                .Include(ec => ec.Employee)
                .ThenInclude(emp => emp!.User)
                .ToListAsync();
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
