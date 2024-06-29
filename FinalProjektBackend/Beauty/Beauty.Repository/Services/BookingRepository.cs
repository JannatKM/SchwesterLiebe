using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            await _context.Bookings!.AddAsync(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            _context.Bookings!.Remove(booking);
        }

        public async Task<Booking> GetBookingAsync(int bookingId)
        {
            return await _context.Bookings
                .SingleOrDefaultAsync(x =>
                x.Id.Equals(bookingId));
        }


        public async Task<Booking> GetBookingAsync(int employeeId, string date, string time)
        {
            return await _context.Bookings
                .Include(x => x.Customer)
                .ThenInclude(c => c.User)
                .Include(x => x.Product)
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x =>
                x.EmployeeId.Equals(employeeId) &&
                x.Date.Equals(date) &&
                x.Time.Equals(time));
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.CustomerId.Equals(userId))
                .Include(b => b.Employee)
                .ThenInclude(e => e.User)
                .Include(b => b.Customer)
                .ThenInclude(c => c.User)
                //.Include(b => b.Appointment)
                //.ThenInclude(a => a.Product)
                .Include(b => b.Product)
                .Include(b => b.Room)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings!
                .Include(b => b.Employee)
                .ThenInclude(e => e!.User)
                .Include(b => b.Customer)
                .ThenInclude(c => c!.User)
                //.Include(b => b.Appointment)
                //.ThenInclude(a => a.Product)
                .Include(b => b.Product)
                .Include(b => b.Room)
                .ToListAsync();
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
