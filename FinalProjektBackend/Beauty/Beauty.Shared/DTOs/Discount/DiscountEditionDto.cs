using Beauty.Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Discount
{
    public class DiscountEditionDto : BaseEntityDto
    {
        [Required]
        public string? StartDate { get; set; }


        [Required]
        public string? EndDate { get; set; }


        [Required]
        public int Percent { get; set; }
    }
}
