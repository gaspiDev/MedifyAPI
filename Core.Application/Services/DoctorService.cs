using AutoMapper;
using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Infrastructure.Data.Repositories;


namespace Core.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private IUserService _userService;
        private IAppointmentRepository _appointmentRepository;
        private IAuth0Service _auth0Service;
        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper, IUserService userService, IAuth0Service auth0Service, IAppointmentRepository appointmentRepository)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _userService = userService;
            _auth0Service = auth0Service;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<DoctorForViewDto>?> ReadDoctors()
        {
            var doctors = await _doctorRepository.ReadAllDoctorsAsync();
            var doctorForView = _mapper.Map<IEnumerable<DoctorForViewDto>>(doctors);
            return doctorForView;
        }

        public async Task<DoctorForViewDto?> ReadById(Guid id)
        {
            var doctor = await _doctorRepository.ReadById(id);
            if (doctor == null)
            {
                return null;
            }
            else
            {
                var doctorForView = _mapper.Map<DoctorForViewDto>(doctor);
                return doctorForView;
            }
        }
        public async Task<DoctorForViewDto?> ReadByUserIdAsync(Guid userId)
        {
            var doctor = await _doctorRepository.ReadByUserIdAsync(userId);
            if (doctor == null)
            {
                return null;
            }
            else
            {
                var doctorForView = _mapper.Map<DoctorForViewDto>(doctor);
                return doctorForView;
            }
        }


        public async Task<IEnumerable<PatientForViewDto>?> ReadPatientsByDoctor(Guid doctorId)
        {
            var patients = await _doctorRepository.ReadPatientsByDoctorAsync(doctorId);
            if (patients == null)
            {
                return null;
            }
            else
            {
                var patientsForView = _mapper.Map<IEnumerable<PatientForViewDto>?>(patients);
                return patientsForView;
            }
        }

        public async Task<string?> CreateDoctorAsync(DoctorForCreationDto dto)
        {
            var userByEmail = await _userService.ReadUserByEmail(dto.User.Email);
            if (userByEmail != null) throw new Exception("Mail already exists");

            var doctorByLicence = await _doctorRepository.ReadByLicenseAsync(dto.LicenseNumber);
            if (doctorByLicence != null) throw new Exception("Licence already exists");

            var doctorByDni = await _doctorRepository.ReadByDniAsync(dto.Dni);
            if (doctorByDni != null) throw new Exception("Dni already exists");

            var auth0Exists = await _auth0Service.UserExists(dto.User.Email);
            if (auth0Exists) throw new Exception("User already exists");

            dto.User.Role = Role.Admin;

            string? auth0Id = null;
            string? userId = null;

            try
            {
                auth0Id = await _auth0Service.CreateUserAsSysAdmin(dto.User);
                if (string.IsNullOrEmpty(auth0Id))
                    throw new Exception("AUTH0_CREATION_FAILED");

                userId = await _userService.CreateUserAsync(dto.User, auth0Id);
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("USER_DB_CREATION_FAILED");

                var doctorEntity = _mapper.Map<Doctor>(dto);
                doctorEntity.UserId = Guid.Parse(userId);

                await _doctorRepository.CreateAsync(doctorEntity);

                return doctorEntity.Id.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateDoctor] Error: {ex.Message}");

                if (!string.IsNullOrEmpty(auth0Id))
                    await _auth0Service.DeleteUserAsync(auth0Id);

                if (!string.IsNullOrEmpty(userId))
                    await _userService.DeleteUserAsync(Guid.Parse(userId));

                throw;
            }
        }

        public async Task<Guid?> UpdateDoctorAsync(Guid id, DoctorForUpdateDto dto)
        {
            var doctor = await _doctorRepository.ReadById(id);
            if (doctor == null)
            {
                return null;
            }

            _mapper.Map(dto, doctor);
            await _doctorRepository.UpdateAsync(doctor);
            return doctor.Id;
        }

        public async Task<Guid?> DeleteDoctorAsync(Guid id)
        {
            var doctor = await _doctorRepository.ReadById(id);
            if (doctor == null)
            {
                return null;
            }
            else
            {
                var appointments = await _appointmentRepository.ReadAppointmentsByDoctorAsync(id);
                if(appointments != null) throw new InvalidOperationException("Cannot delete doctor with existing appointments.");

                await _doctorRepository.DeleteDoctor(doctor.Id);
                return doctor.Id;
                
            }
        }



    }
}
