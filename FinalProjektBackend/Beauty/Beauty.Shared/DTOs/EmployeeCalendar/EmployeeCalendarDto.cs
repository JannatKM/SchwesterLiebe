using Beauty.Shared.DTOs.Base;
using Beauty.Shared.DTOs.Employee;

namespace Beauty.Shared.DTOs.EmployeeCalendar
{
    public class EmployeeCalendarDto : BaseEntityDto
    {
        public string? Date { get; set; }

        public int BookTime { get; set; }

        public List<int>? AllTimes { get; set; }

        public bool IsVacation { get; set; }

        public string? VacationDescription { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }
    }
}
