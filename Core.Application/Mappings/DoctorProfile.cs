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
            CreateMap<Doctor, DoctorForViewDto>();
            CreateMap<DoctorForCreationDto, Doctor>()
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<DoctorForUpdateDto, Doctor>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
