using Beauty.Shared.DTOs.Base;

namespace Beauty.Shared.DTOs.EmployeeTime
{
    public class EmployeeTimeDto : BaseEntityDto
    {
        public string? Date { get; set; }

        public string? Time { get; set; }

        public bool IsReserved { get; set; }

        public int EmployeeId { get; set; }
    }
}
