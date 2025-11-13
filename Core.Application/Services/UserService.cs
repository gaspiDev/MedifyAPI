using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.DTOs.UserDTO;
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

        public async Task<UserForViewDto?> ReadUserByEmail(string email)
        {
            var user = await _userRepository.ReadUserByEmailAsync(email);
            return user == null
                ? null
                : _mapper.Map<UserForViewDto>(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.ReadByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            try
            {
            await _userRepository.DeleteUserAsync(id);
            return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return false;
            }
        }


    }
}
