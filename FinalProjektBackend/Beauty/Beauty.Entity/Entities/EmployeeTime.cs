using Beauty.Entity.Base;

namespace Beauty.Entity.Entities;

public class EmployeeTime : BaseEntity
{
    public string? Date { get; set; }

    public string? Time { get; set; }

    public bool IsReserved { get; set; }

    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
