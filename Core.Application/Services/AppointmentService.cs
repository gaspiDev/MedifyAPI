using AutoMapper;
using Core.Application.DTOs.AppointmentDTO;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentForViewDto>?> ReadAppointments()
        {
            var appointments = await _appointmentRepository.ReadAsync();
            var appointmentForView = _mapper.Map<IEnumerable<AppointmentForViewDto>>(appointments);
            return appointmentForView;
        }

        public async Task<AppointmentForViewDto?> ReadById(Guid id)
        {
            var appointment = await _appointmentRepository.ReadByIdAsync(id);
            if (appointment == null)
            {
                return null;
            }
            else
            {
                var appointmentForView = _mapper.Map<AppointmentForViewDto>(appointment);
                return appointmentForView;
            }
        }

        public async Task<IEnumerable<AppointmentForViewAsDoctorDto>?> ReadAppointmentsByDoctor(Guid doctorId)
        {
            var appointments = await _appointmentRepository.ReadAppointmentsByDoctorAsync(doctorId);
            if (appointments == null)
            {
                return null;
            }
            else
            {
                var appointmentsForView = _mapper.Map<IEnumerable<AppointmentForViewAsDoctorDto>>(appointments);
                return appointmentsForView;
            }
        }

        public async Task<IEnumerable<AppointmentForViewAsPatientDto>?> ReadAppointmentsByPatient(Guid patientId)
        {
            var appointments = await _appointmentRepository.ReadByPatientIdAsync(patientId);
            if (appointments == null)
            {
                return null;
            }
            else
            {
                var appointmentsForView = _mapper.Map<IEnumerable<AppointmentForViewAsPatientDto>>(appointments);
                return appointmentsForView;
            }
        }

        public async Task<AppointmentForViewDto> CreateAppointment(AppointmentForCreationDto appointmentDto)
        {
            var appointmentEntity = _mapper.Map<Appointment>(appointmentDto);
            try
            {
                await _appointmentRepository.CreateAsync(appointmentEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the appointment: {ex.Message}");
                throw;
            }
            var appointmentForView = _mapper.Map<AppointmentForViewDto>(appointmentEntity);
            return appointmentForView;
        }



    }

}

