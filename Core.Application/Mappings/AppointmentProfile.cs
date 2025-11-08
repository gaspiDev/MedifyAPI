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
            CreateMap<AppointmentForCreationDto, Appointment>();
            CreateMap<AppointmentForUpdateDto, Appointment>();
        }
    }
}
