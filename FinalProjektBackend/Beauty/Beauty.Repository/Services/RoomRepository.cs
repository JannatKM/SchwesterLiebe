using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateRoomAsync(Room room)
        {
            await _context.Rooms!.AddAsync(room);
        }

        public void DeleteRoom(Room room)
        {
            _context.Rooms!.Remove(room);
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
            return await _context.Rooms
                .SingleOrDefaultAsync(x => x.Id.Equals(roomId));
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms!.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
