using Beauty.Shared.DTOs.Base;

namespace Beauty.Shared.DTOs.Discount
{
    public class DiscountDto : BaseEntityDto
    {
        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

        public int Percent { get; set; }
    }
}
