using Beauty.Shared.DTOs.Base;

namespace Beauty.Shared.DTOs.EmployeeCalendar
{
    public class EmployeeCalendarForEditionDto : BaseEntityDto
    {
        public string? Date { get; set; }

        public int BookTime { get; set; }

        public bool IsVacation { get; set; }

        public string? VacationDescription { get; set; }

        public int EmployeeId { get; set; }
    }
}
