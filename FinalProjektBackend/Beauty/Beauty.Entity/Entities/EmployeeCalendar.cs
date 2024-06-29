using Beauty.Entity.Base;

namespace Beauty.Entity.Entities;

public class EmployeeCalendar : BaseEntity
{
    public string? Date { get; set; }

    public int? BookTime { get; set; }

    public bool IsVacation { get; set; }

    public string? VacationDescription { get; set; }

    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
