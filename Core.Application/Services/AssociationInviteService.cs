using AutoMapper;
using Core.Application.DTOs.Association;
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
    public class AssociationInviteService : IAssociationInviteService
    {
        private readonly IAssociationInviteRepository _inviteRepository;
        private readonly IDoctorPatientRepository _doctorPatientRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        public AssociationInviteService(IAssociationInviteRepository inviteRepository, 
            IDoctorPatientRepository doctorPatientRepository, IMapper mapper,
            IUserRepository userRepository, IPatientRepository patientRepository)
        {
            _inviteRepository = inviteRepository;
            _doctorPatientRepository = doctorPatientRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _patientRepository = patientRepository;
        }

        public async Task<InviteForViewDto> CreateInviteByCodeAsync(CreateInvitationDto dto)
        {
            var invite = new AssociationInvite
            {
                Id = Guid.NewGuid(),
                DoctorId = dto.DoctorId,
                InviteCode = GenerateCode(),
                QRToken = null,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                SentAt = DateTime.UtcNow
            };

            await _inviteRepository.CreateAsync(invite);
            return _mapper.Map<InviteForViewDto>(invite);
        }



        public async Task<InviteForViewDto> CreateInviteByQrAsync(CreateInvitationDto dto)
        {
            var invite = new AssociationInvite
            {
                Id = Guid.NewGuid(),
                DoctorId = dto.DoctorId,
                InviteCode = null,
                QRToken = GenerateQrToken(),
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                SentAt = DateTime.UtcNow
            };

            await _inviteRepository.CreateAsync(invite);
            return _mapper.Map<InviteForViewDto>(invite);
        }

        public async Task<InviteForViewDto?> ValidateByCodeAsync(string code)
        {
            var invite = await _inviteRepository.ReadByCodeAsync(code);
            return invite == null ? null : _mapper.Map<InviteForViewDto>(invite);
        }

        public async Task<InviteForViewDto?> ValidateByQrAsync(string token)
        {
            var invite = await _inviteRepository.ReadQRAsync(token);
            return invite == null ? null : _mapper.Map<InviteForViewDto>(invite);
        }


        public async Task<AssociationForViewDto> AcceptInviteAsync(AcceptInvitationDto dto)
        {
            var invite = await _inviteRepository.ReadByIdAsync(dto.InviteId)
                         ?? throw new Exception("Invite not found");

            if (invite.IsAccepted)
                throw new Exception("This invitation has already been used.");

            if (invite.ExpiresAt <= DateTime.UtcNow)
                throw new Exception("This invitation has expired.");

            var active = await _doctorPatientRepository.ReadExistingAssociationsAsync(invite.DoctorId, dto.PatientId);
            if (active != null)
                throw new Exception("This doctor and patient are already associated.");


            var method = invite.InviteCode != null
                ? AssociationMethod.Request
                : AssociationMethod.QRCode;

            var association = new DoctorPatient
            {
                Id = Guid.NewGuid(),
                DoctorId = invite.DoctorId,
                PatientId = dto.PatientId,
                Method = method,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _doctorPatientRepository.CreateAsync(association);

            invite.IsAccepted = true;
            invite.AcceptedAt = DateTime.UtcNow;
            invite.PatientId = dto.PatientId;

            await _inviteRepository.UpdateAsync(invite);

            return _mapper.Map<AssociationForViewDto>(association);
        }

        public async Task<AssociationForViewDto> AcceptByCodeAsync(string inviteCode, string auth0Id)
        {
            var invite = await _inviteRepository.ReadByCodeAsync(inviteCode)
                         ?? throw new Exception("Invite not found");

            if (invite.IsAccepted)
                throw new Exception("This invitation has already been used.");

            if (invite.ExpiresAt <= DateTime.UtcNow)
                throw new Exception("This invitation has expired.");

            var user = await _userRepository.ReadByAuth0IdAsync(auth0Id)
                       ?? throw new Exception("User not found.");

            var patient = await _patientRepository.ReadByIdAsync(user.Id)
                          ?? throw new Exception("Patient profile not found.");

            var existing = await _doctorPatientRepository.ReadExistingAssociationsAsync(invite.DoctorId, patient.Id);
            if (existing != null)
                throw new Exception("This doctor and patient are already associated.");

            
            var method = AssociationMethod.Request; 

            var association = new DoctorPatient
            {
                Id = Guid.NewGuid(),
                DoctorId = invite.DoctorId,
                PatientId = patient.Id,
                Method = method,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _doctorPatientRepository.CreateAsync(association);

            invite.IsAccepted = true;
            invite.AcceptedAt = DateTime.UtcNow;
            invite.PatientId = patient.Id;

            await _inviteRepository.UpdateAsync(invite);

            return _mapper.Map<AssociationForViewDto>(association);
        }

        public async Task<AssociationForViewDto> AcceptByQrAsync(string qrToken, string auth0Id)
        {
            var invite = await _inviteRepository.ReadQRAsync(qrToken)
                         ?? throw new Exception("Invite not found");

            if (invite.IsAccepted)
                throw new Exception("This invitation has already been used.");

            if (invite.ExpiresAt <= DateTime.UtcNow)
                throw new Exception("This invitation has expired.");

            var user = await _userRepository.ReadByAuth0IdAsync(auth0Id)
                       ?? throw new Exception("User not found.");

            var patient = await _patientRepository.ReadByIdAsync(user.Id)
                          ?? throw new Exception("Patient profile not found.");

            var existing = await _doctorPatientRepository.ReadExistingAssociationsAsync(invite.DoctorId, patient.Id);
            if (existing != null)
                throw new Exception("This doctor and patient are already associated.");

            var method = AssociationMethod.QRCode;

            var association = new DoctorPatient
            {
                Id = Guid.NewGuid(),
                DoctorId = invite.DoctorId,
                PatientId = patient.Id,
                Method = method,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _doctorPatientRepository.CreateAsync(association);

            invite.IsAccepted = true;
            invite.AcceptedAt = DateTime.UtcNow;
            invite.PatientId = patient.Id;

            await _inviteRepository.UpdateAsync(invite);

            return _mapper.Map<AssociationForViewDto>(association);
        }


        private string GenerateCode() =>
            Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

        private string GenerateQrToken() =>
            Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }

}
