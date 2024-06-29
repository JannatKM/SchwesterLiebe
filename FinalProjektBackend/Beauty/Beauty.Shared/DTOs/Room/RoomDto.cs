using Beauty.Shared.DTOs.Base;

namespace Beauty.Shared.DTOs.Room
{
    public class RoomDto : BaseEntityDto
    {
        public string? Name { get; set; }

        public bool IsDown { get; set; }

        public string? Description { get; set; }
    }
}
