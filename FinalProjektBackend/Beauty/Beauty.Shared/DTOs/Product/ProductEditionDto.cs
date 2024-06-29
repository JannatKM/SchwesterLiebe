using Beauty.Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Product
{
    public class ProductEditionDto : BaseEntityDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Name { get; set; }


        [Required]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters")]
        public string? Description { get; set; }


        [Required]
        public int Duration { get; set; }


        [Required]
        public decimal Price { get; set; }
    }
}
