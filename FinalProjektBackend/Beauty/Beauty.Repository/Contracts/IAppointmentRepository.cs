using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();

        Task<IEnumerable<Appointment>> GetAppointmentsByEmployeeIdAsync(int id);

        Task<IEnumerable<Appointment>> GetLastThreeAppointmentsAsync();

        Task<Appointment?> GetAppointmentAsync(int? appointmentId);

        Task CreateAppointmentAsync(Appointment appointment);

        void DeleteAppointment(Appointment appointment);

        Task SaveAsync();
    }
}
