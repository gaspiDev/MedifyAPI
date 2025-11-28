using AutoMapper;
using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IAuth0Service _auth0Service;
        private readonly IUserService _userService;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IAuth0Service auth0Service, IUserService userService,
            IPatientRepository patientRepository, IMapper mapper)
        {
            _auth0Service = auth0Service;
            _userService = userService;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PatientForViewDto>?> ReadPatients()
        {
            var patients = await _patientRepository.ReadAllPatientsAsync();
            var patientForView = _mapper.Map<IEnumerable<PatientForViewDto>>(patients);
            return patientForView;
        }

        public async Task<PatientForViewDto?> ReadById(Guid id)
        {
            var patient = await _patientRepository.ReadUserByIdAsync(id);
            if (patient == null)
            {
                return null;
            }
            else
            {
                var patientForView = _mapper.Map<PatientForViewDto>(patient);
                return patientForView;
            }
        }

        public async Task<IEnumerable<DoctorForViewDto>?> ReadDoctorsByPatient(Guid patientId)
        {
            var doctors = await _patientRepository.ReadDoctorsByPatientAsync(patientId);
            if (doctors == null)
            {
                return null;
            }
            else
            {
                var doctorsForView = _mapper.Map<IEnumerable<DoctorForViewDto>>(doctors);
                return doctorsForView;
            }
        }
        public async Task<string?> CreatePatientAsync(PatientForCreationDto dto)
        { 
            var userByEmail = await _userService.ReadUserByEmail(dto.User.Email);
            if (userByEmail != null) throw new Exception("Mail already exists");

            var patientByDNI = await _patientRepository.ReadByDniAsync(dto.Dni);
            if (patientByDNI != null) throw new Exception("Dni already exists");

            dto.User.Role = Role.User;
            string? auth0Id = null;
            string? userId = null;

            try
            {
                auth0Id = await _auth0Service.CreateUserAsSysAdmin(dto.User);
                if (string.IsNullOrEmpty(auth0Id)) throw new Exception("AUTH0_CREATION_FAILED");

                userId = await _userService.CreateUserAsync(dto.User, auth0Id);
                if (string.IsNullOrEmpty(userId)) throw new Exception("USER_DB_CREATION_FAILED");

                var patient = _mapper.Map<Patient>(dto);
                patient.UserId = Guid.Parse(userId);

                await _patientRepository.CreateAsync(patient);

                return patient.Id.ToString();
            }
            catch
            {
                if (!string.IsNullOrEmpty(auth0Id))
                    await _auth0Service.DeleteUserAsync(auth0Id);

                if (!string.IsNullOrEmpty(userId))
                    await _userService.DeleteUserAsync(Guid.Parse(userId));

                throw; 
            }
        }

        public async Task<Guid?> UpdatePatientAsync(Guid id, PatientForUpdateDto dto)
        {
            var patient = await _patientRepository.ReadByIdAsync(id);
            if (patient == null)
            {
                return null;
            }

            _mapper.Map(dto, patient);
            await _patientRepository.UpdateAsync(patient);
            //await _user_service.UpdateUserAsync(dto.User);
            return patient.Id;
        }



    }
}
