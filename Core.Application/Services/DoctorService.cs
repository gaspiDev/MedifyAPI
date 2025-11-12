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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private IUserService _userService;
        private IAuth0Service _auth0Service;
        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper, IUserService userService, IAuth0Service auth0Service)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _userService = userService;
            _auth0Service = auth0Service;
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
            dto.User.Role = Role.Admin;
            var auth0Id = await _auth0Service.CreateUserAsSysAdmin(dto.User);

            var userId = await _userService.CreateUserAsync(dto.User, auth0Id);
            Console.WriteLine($"Auth0Id = {auth0Id}");
            var doctorEntity = _mapper.Map<Doctor>(dto);
            doctorEntity.UserId = Guid.Parse(userId);
            Console.WriteLine($"Doctor.UserId = {doctorEntity.UserId}");
            await _doctorRepository.CreateAsync(doctorEntity);

            return doctorEntity.Id.ToString();
        }


    }
}
