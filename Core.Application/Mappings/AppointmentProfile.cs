using AutoMapper;
using Core.Application.DTOs.AppointmentDTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentForViewDto>();
            CreateMap<Appointment, AppointmentForViewAsDoctorDto>()
                 .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
               .ForMember(dest => dest.DoctorName,
                    opt => opt.MapFrom(src => src.Doctor != null
                        ? $"{src.Doctor.FirstName} {src.Doctor.LastName}".Trim()
                        : string.Empty))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId));

            CreateMap<Appointment, AppointmentForViewAsPatientDto>()
               .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
               .ForMember(dest => dest.PatientName,
                   opt => opt.MapFrom(src => src.Patient != null
                       ? $"{src.Patient.FirstName} {src.Patient.LastName}".Trim()
                       : string.Empty))
               .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId));

            CreateMap<AppointmentForCreationDto, Appointment>();
            CreateMap<AppointmentForUpdateDto, Appointment>();
        }
    }
}
