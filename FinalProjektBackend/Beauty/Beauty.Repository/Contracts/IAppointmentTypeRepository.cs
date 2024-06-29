using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IAppointmentTypeRepository
    {
        Task<IEnumerable<AppointmentType>> GetAppointmentTypesAsync();

        Task<AppointmentType> GetAppointmentTypeAsync(int appointmentTypeId);

        Task CreateAppointmentTypeAsync(AppointmentType appointmentType);

        void DeleteAppointmentType(AppointmentType appointmentType);

        Task SaveAsync();
    }
}
