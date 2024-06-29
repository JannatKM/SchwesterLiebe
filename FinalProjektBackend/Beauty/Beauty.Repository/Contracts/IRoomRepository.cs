using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync();

        Task<Room> GetRoomAsync(int roomId);

        Task CreateRoomAsync(Room room);

        void DeleteRoom(Room room);

        Task SaveAsync();
    }
}
