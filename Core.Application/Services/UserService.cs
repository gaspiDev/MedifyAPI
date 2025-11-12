using AutoMapper;
using Core.Application.DTOs.User;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateUserAsync(UserForCreationDto userDto, string auth0id)
        {
            try
            {
                var userEntity = _mapper.Map<User>(userDto);
                userEntity.Id = Guid.NewGuid();
                userEntity.IsActive = true;
                userEntity.Auth0Id = auth0id;
                await _userRepository.CreateAsync(userEntity);
                return userEntity.Id.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw;
            }

        }

    }
}
