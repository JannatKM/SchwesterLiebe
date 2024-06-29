using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Customer
{
    public class CustomerCreationDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
