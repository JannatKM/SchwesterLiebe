using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Discount
{
    public class DiscountCreationDto
    {
        [Required]
        public string? StartDate { get; set; }


        [Required]
        public string? EndDate { get; set; }


        [Required]
        public int Percent { get; set; }
    }
}
