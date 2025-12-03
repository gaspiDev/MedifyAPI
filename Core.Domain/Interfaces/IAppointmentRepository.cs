using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<IEnumerable<Appointment?>> ReadAppointmentsByDoctorAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> ReadAsync();
        Task<Appointment?> ReadByIdAsync(Guid id);
        Task<IEnumerable<Appointment?>> ReadByPatientIdAsync(Guid patientId);
    }
}