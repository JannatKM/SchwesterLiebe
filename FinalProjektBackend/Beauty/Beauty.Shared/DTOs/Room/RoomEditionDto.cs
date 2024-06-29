using Beauty.Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Room
{
    public class RoomEditionDto : BaseEntityDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Name { get; set; }

        public bool IsDown { get; set; }

        public string? Description { get; set; }
    }
}
