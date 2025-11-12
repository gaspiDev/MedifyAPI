using Core.Application.DTOs.User;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
