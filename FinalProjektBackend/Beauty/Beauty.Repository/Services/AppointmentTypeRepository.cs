using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class AppointmentTypeRepository : IAppointmentTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAppointmentTypeAsync(AppointmentType appointmentType)
        {
            await _context.AppointmentTypes.AddAsync(appointmentType);
        }

        public void DeleteAppointmentType(AppointmentType appointmentType)
        {
            _context.AppointmentTypes.Remove(appointmentType);
        }

        public async Task<AppointmentType> GetAppointmentTypeAsync(int appointmentTypeId)
        {
            return await _context.AppointmentTypes
                .SingleOrDefaultAsync(x => x.Id.Equals(appointmentTypeId));
        }

        public async Task<IEnumerable<AppointmentType>> GetAppointmentTypesAsync()
        {
            return await _context.AppointmentTypes.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
