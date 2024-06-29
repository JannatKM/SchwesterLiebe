using Beauty.Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Customer
{
    public class CustomerEditionDto : BaseEntityDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
