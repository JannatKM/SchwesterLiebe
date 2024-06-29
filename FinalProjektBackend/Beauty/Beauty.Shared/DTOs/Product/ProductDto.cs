using Beauty.Shared.DTOs.Base;

namespace Beauty.Shared.DTOs.Product
{
    public class ProductDto : BaseEntityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }
    }
}
