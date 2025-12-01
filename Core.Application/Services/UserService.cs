using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.DTOs.UserDTO;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        public UserService(IUserRepository userRepository, IMapper mapper, IDoctorRepository doctorRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
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

        //public async Task<UserForViewDto> CreateFromAuth0Async(string auth0Id, string email)
        //{
        //    var entity = new User
        //    {
        //        Id = Guid.NewGuid(),
        //        Email = email,
        //        Auth0Id = auth0Id,
        //        Role = Role.Admin,      
        //        IsActive = true,
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    await _userRepository.CreateAsync(entity);

        //    return _mapper.Map<UserForViewDto>(entity);
        //}
        public async Task<UserForViewDto> CreateFromAuth0Async(string auth0Id, string email)
        {
            // 1) Crear el User
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Auth0Id = auth0Id,
                // elegí el rol que corresponda:
                // Role = Role.Admin;
                Role = Role.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);

            // 2) Crear el Doctor asociado (perfil vacío por ahora)
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FirstName = string.Empty,
                LastName = string.Empty,
                Specialty = string.Empty,
                LicenseNumber = string.Empty,
                Adress = string.Empty,   // (ojo con el typo, usá el nombre exacto de tu entidad)
                Dni = 0                  // poné 0 o algún valor neutro que acepte la DB
            };

            await _doctorRepository.CreateAsync(doctor);

            // 3) Devolvés el user para el front
            return _mapper.Map<UserForViewDto>(user);
        }


        public async Task<UserForViewDto?> ReadUserByEmail(string email)
        {
            var user = await _userRepository.ReadUserByEmailAsync(email);
            return user == null
                ? null
                : _mapper.Map<UserForViewDto>(user);
        }

        public async Task<UserForViewDto?> ReadByAuth0IdAsync(string auth0Id)
        {
            var user = await _userRepository.ReadByAuth0IdAsync(auth0Id);
            return _mapper.Map<UserForViewDto?>(user);
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
