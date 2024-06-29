using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsAsync();

        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);

        Task<Booking> GetBookingAsync(int bookingId);

        Task<Booking> GetBookingAsync(int employeeId, string date, string time);

        Task CreateBookingAsync(Booking booking);

        void DeleteBooking(Booking booking);

        Task SaveAsync();
    }
}
