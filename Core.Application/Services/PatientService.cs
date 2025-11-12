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
            dto.User.Role = Role.User;
            var auth0Id = await _auth0Service.CreateUserAsSysAdmin(dto.User);

            var userId = await _userService.CreateUserAsync(dto.User, auth0Id);
            var patientEntity = _mapper.Map<Patient>(dto);
            patientEntity.UserId = Guid.Parse(userId);

            await _patientRepository.CreateAsync(patientEntity);

            return patientEntity.Id.ToString();
        }


    }
}
