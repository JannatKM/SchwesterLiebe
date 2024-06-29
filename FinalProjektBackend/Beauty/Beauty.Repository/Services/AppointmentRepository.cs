using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments!.AddAsync(appointment);
        }


        public void DeleteAppointment(Appointment appointment)
        {
            _context.Appointments!.Remove(appointment);
        }


        public async Task<Appointment?> GetAppointmentAsync(int? appointmentId)
        {
            return await _context.Appointments!
                .Include(a => a.Room)
                .Include(a => a.Product)
                .Include(a => a.AppointmentType)
                .Include(a => a.Employee)
                .ThenInclude(emp => emp!.User)
                .Include(a => a.Discount)
                .FirstOrDefaultAsync(x => x.Id.Equals(appointmentId));
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await _context.Appointments!
                .Where(a => a.IsSelected.Equals(false))
                .Include(a => a.Room)
                .Include(a => a.Product)
                .Include(a => a.AppointmentType)
                .Include(a => a.Employee)
                .ThenInclude(emp => emp!.User)
                .Include(a => a.Discount)
                .ToListAsync();
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsByEmployeeIdAsync(int id)
        {
            return await _context.Appointments!
                .Where(a => a.EmployeeId.Equals(id) &&
                                              a.IsSelected.Equals(false))
                .Include(a => a.Room)
                .Include(a => a.Product)
                .Include(a => a.AppointmentType)
                .Include(a => a.Employee)
                .ThenInclude(emp => emp!.User)
                .Include(a => a.Discount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetLastThreeAppointmentsAsync()
        {
            return await _context.Appointments!
                .Where(a => a.IsSelected.Equals(false))
                .Take(3)
                .Include(a => a.Room)
                .Include(a => a.Product)
                .Include(a => a.AppointmentType)
                .Include(a => a.Employee)
                .ThenInclude(emp => emp!.User)
                .Include(a => a.Discount)
                .ToListAsync();
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
