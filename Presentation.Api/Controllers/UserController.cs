using Core.Application.DTOs.User;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuth0Service _auth0Service;
        private readonly IUserService _userService;
        public UserController(IAuth0Service auth0Service, IUserService userService)
        {
            _auth0Service = auth0Service;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto user)
        {
            try
            {
                var auth0Id = await _auth0Service.CreateUserAsSysAdmin(user);

                await _userService.CreateUserAsync(user, auth0Id);

                return Ok(new { message = "User created successfully", auth0Id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user: {ex.Message}");
            }
        }

        [HttpDelete("{auth0Id}")]
        public async Task<IActionResult> DeleteUser(string auth0Id)
        {
            try
            {
                await _auth0Service.DeleteUserAsync(auth0Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Error deleting user: {ex.Message}");
            }
        }

        //[HttpGet("me")]
        //[Authorize]
        //public async Task<IActionResult> GetMyData()
        //{
        //    // Usar el claim que ASP.NET mapea por defecto
        //    var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    // O, si querés hacerlo “hardcodeado”:
        //    // var auth0Id = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        //    if (auth0Id is null)
        //        return Unauthorized("Auth0Id (sub) no encontrado en los claims.");

        //    var user = await _userService.ReadByAuth0IdAsync(auth0Id);

        //    if (user is null)
        //    {
        //        return NotFound("User not found in Medify database");
        //    }

        //    return Ok(user); 
        //}
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyData()
        {
            var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var email =
                User.FindFirst(ClaimTypes.Email)?.Value ??
                User.FindFirst("email")?.Value ??
                User.FindFirst("https://medify-api/email")?.Value; 

            if (auth0Id is null)
                return Unauthorized("Auth0Id (sub) no encontrado en los claims.");

            var user = await _userService.ReadByAuth0IdAsync(auth0Id);

            if (user is null)
            {
                if (string.IsNullOrEmpty(email))
                    return BadRequest("User not found and no email claim available to auto-create.");

                user = await _userService.CreateFromAuth0Async(auth0Id, email);
            }

            return Ok(user);
        }

    }
}
