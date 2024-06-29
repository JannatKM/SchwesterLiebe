using Beauty.Shared.DTOs.Base;

namespace Beauty.Shared.DTOs.Customer
{
    public class CustomerDto : BaseEntityDto
    {
        public int UserId { get; set; }
        public Entity.Entities.User? User { get; set; }
    }
}
