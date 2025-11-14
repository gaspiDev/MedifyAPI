using AutoMapper;
using Core.Application.DTOs.AssociationDTO;
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
    public class DoctorPatientService : IDoctorPatientService
    {
        private readonly IDoctorPatientRepository _doctorPatientRepository;
        private readonly IMapper _mapper;
        public DoctorPatientService(IDoctorPatientRepository doctorPatientRepository, IMapper mapper)
        {
            _doctorPatientRepository = doctorPatientRepository;
            _mapper = mapper;
        }

        public async Task<AssociationForViewDto>? CreateAssociationAsync(AssociationAsSysAdminDto dto)
        {

            var exists = await _doctorPatientRepository.ReadExistingAssociationsAsync(dto.DoctorId, dto.PatientId);
            if (exists != null)
                throw new Exception("This doctor and patient are already associated.");

            var inactive = await _doctorPatientRepository.ReadInactiveAsync(dto.DoctorId, dto.PatientId);
            if (inactive != null)
            {
                inactive.IsActive = true;
                inactive.UnassignedAt = null;
                inactive.AssignedAt = DateTime.UtcNow;
                inactive.Method = AssociationMethod.Manual;

                await _doctorPatientRepository.UpdateAsync(inactive);
                return _mapper.Map<AssociationForViewDto>(inactive);
            }

            var association = new DoctorPatient
            {
                Id = Guid.NewGuid(),
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AssignedAt = DateTime.UtcNow,
                Method = AssociationMethod.Manual,
                IsActive = true
            };
            try
            {
                await _doctorPatientRepository.CreateAsync(association);
                return _mapper.Map<AssociationForViewDto>(association);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating association: " + ex.Message);
            }
        }

        public async Task<AssociationForViewDto?> UnassignAssociationAsync(Guid associationId)
        {
            var association = await _doctorPatientRepository.ReadAssociationByIdAsync(associationId);
            if (association == null || !association.IsActive)
                throw new Exception("Active association not found.");
            association.IsActive = false;
            association.UnassignedAt = DateTime.UtcNow;
            try
            {
                await _doctorPatientRepository.UpdateAsync(association);
                return _mapper.Map<AssociationForViewDto>(association);
            }
            catch (Exception ex)
            {
                throw new Exception("Error unassigning association: " + ex.Message);
            }
        }

    }
}
