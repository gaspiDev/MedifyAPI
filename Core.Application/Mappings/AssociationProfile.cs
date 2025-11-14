using AutoMapper;
using Core.Application.DTOs.AssociationDTO;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class AssociationProfile : Profile
    {
        public AssociationProfile()
        {
            CreateMap<AssociationInvite, InviteForViewDto>();

            CreateMap<DoctorPatient, AssociationForViewDto>()
                .ForMember(dest => dest.Method,
                           opt => opt.MapFrom(src => src.Method.ToString()));
        }
    }
}
