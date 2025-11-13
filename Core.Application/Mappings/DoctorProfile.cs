using AutoMapper;
using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.User;
using Core.Application.DTOs.UserDTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<Doctor, DoctorForViewDto>();
            //.ForMember(dest => dest.User,
            //opt => opt.MapFrom(src => src.));
            //CreateMap<DoctorForCreationDto, Doctor>();
            CreateMap<DoctorForCreationDto, Doctor>()
    .ForMember(dest => dest.User, opt => opt.Ignore()); // 👈 evita crear un User anidado

        }
    }
}
