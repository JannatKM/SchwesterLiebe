using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.User
{
    public class UserCreationDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? FirstName { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? LastName { get; set; }


        [Required]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters")]
        public string? Password { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Email { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Telephone { get; set; }


        [Required]
        public int RoleId { get; set; }
    }
}
