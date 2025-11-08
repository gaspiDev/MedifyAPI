using AutoMapper;
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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<User, UserForViewDto>();
        }
    }
}
