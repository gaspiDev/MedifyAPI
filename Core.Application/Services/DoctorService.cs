using AutoMapper;
using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;
using Core.Application.Interfaces;
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
        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
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


    }
}
