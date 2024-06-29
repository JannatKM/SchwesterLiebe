using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IEmployeeTimeRepository
    {
        Task<IEnumerable<EmployeeTime>> GetEmployeeTimesAsync();

        Task<IEnumerable<EmployeeTime>> GetEmployeeTimesByEmployeeIdAsync(int id);

        Task<IEnumerable<EmployeeTime>> GetEmployeeTimesByDateAsync(string date);

        Task<IEnumerable<EmployeeTime>> GetEmployeeTimesByDateAsync(int id,string date);

        Task <EmployeeTime> GetEmployeeTimeByIdAsync(int id);

        Task<EmployeeTime?> GetEmployeeTimeByDetailsAsync(int employeeId, string date, int time);

        Task CreateEmployeeTimeAsync(EmployeeTime employeeTime);

        void DeleteEmployeeTime(EmployeeTime employeeTime);

        Task SaveAsync();
    }
}
