using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IEmployeeCalendarRepository
    {
        Task<IEnumerable<EmployeeCalendar>> GetEmployeeCalendarsAsync();

        Task<IEnumerable<EmployeeCalendar>> GetEmployeeCalendarsByEmployeeIdAsync(int id);

        Task<IEnumerable<EmployeeCalendar>> GetDateByEmployeeIdAsync(int id);

        Task<EmployeeCalendar?> GetEmployeeCalendarAsync(int? id);

        Task CreateEmployeeCalendarAsync(EmployeeCalendar employeeCalendar);

        void DeleteEmployeeCalendar(EmployeeCalendar employeeCalendar);

        Task SaveAsync();
    }
}
