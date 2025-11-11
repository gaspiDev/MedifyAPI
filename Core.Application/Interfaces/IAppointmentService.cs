using Core.Application.DTOs.AppointmentDTO;

namespace Core.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentForViewDto> CreateAppointment(AppointmentForCreationDto appointmentDto);
        Task<IEnumerable<AppointmentForViewDto>?> ReadAppointments();
        Task<IEnumerable<AppointmentForViewAsDoctorDto>?> ReadAppointmentsByDoctor(Guid doctorId);
        Task<IEnumerable<AppointmentForViewAsPatientDto>?> ReadAppointmentsByPatient(Guid patientId);
        Task<AppointmentForViewDto?> ReadById(Guid id);
    }
}