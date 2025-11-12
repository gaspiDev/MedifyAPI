using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.Interfaces;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class Auth0Service : IAuth0Service
    {
        private readonly IAuth0Repository _auth0Repository;
        private readonly IMapper _mapper;
        public Auth0Service(IAuth0Repository auth0Repository, IMapper mapper)
        {
            _auth0Repository = auth0Repository;
            _mapper = mapper;
        }

        //public async Task<string> CreateUserAsync(string email, string password)
        //{
        //    try
        //    {
        //        var userId = await _auth0Repository.CreateUserAsync(email, password);
        //        return userId;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error creating user in Auth0: {ex.Message}");
        //        throw; 
        //    }
        //}
        public async Task<string> CreateUserAsSysAdmin(UserForCreationDto userDto)
        {
            try
            {
                var userId = await _auth0Repository.CreateUserAsync(userDto.Email, userDto.Password);
                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating SysAdmin user in Auth0: {ex.Message}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteUserAsync(string userId)
        {
            try
            {
                var response = await _auth0Repository.DeleteUserAsync(userId);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to delete user in Auth0. Status Code: {response.StatusCode}");
                }
                return response;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user in Auth0: {ex.Message}");
                throw;
            }
        }
    }
}
