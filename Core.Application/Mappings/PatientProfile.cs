using AutoMapper;
using Core.Application.DTOs.PatientDTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientForViewDto>();
            CreateMap<PatientForCreationDto, PatientForViewDto>();
        }
    }
}
