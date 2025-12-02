using Core.Application.DTOs.Association;
using Core.Application.DTOs.AssociationDTO;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AssociationController : Controller
    {
        private readonly IAssociationInviteService _associationService;

        public AssociationController(IAssociationInviteService associationService)
        {
            _associationService = associationService;
        }

        [HttpPost("invite/code")]
        public async Task<IActionResult> CreateInviteByCode([FromBody] CreateInvitationDto dto)
        {
            var result = await _associationService.CreateInviteByCodeAsync(dto);
            return Ok(result);
        }

        [HttpPost("invite/qr")]
        public async Task<IActionResult> CreateInviteByQr([FromBody] CreateInvitationDto dto)
        {
            var result = await _associationService.CreateInviteByQrAsync(dto);
            return Ok(result);
        }

        [HttpGet("validate/code/{code}")]
        public async Task<IActionResult> ValidateByCode(string code)
        {
            var result = await _associationService.ValidateByCodeAsync(code);
            if (result == null)
                return NotFound("Invalid or expired invite code.");

            return Ok(result);
        }


        [HttpGet("validate/qr/{token}")]
        public async Task<IActionResult> ValidateByQr(string token)
        {
            var result = await _associationService.ValidateByQrAsync(token);
            if (result == null)
                return NotFound("Invalid or expired QR invite.");

            return Ok(result);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptInvite([FromBody] AcceptInvitationDto dto)
        {
            var result = await _associationService.AcceptInviteAsync(dto);
            return Ok(result);
        }

        [HttpPost("accept/code")]
        public async Task<IActionResult> AcceptByCode([FromBody] AcceptByCodeDto dto)
        {
            var auth0Id =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst("sub")?.Value;

            if (auth0Id is null)
                return Unauthorized("Auth0Id (sub) not found in token.");

            var result = await _associationService.AcceptByCodeAsync(dto.InviteCode, auth0Id);
            return Ok(result);
        }

        [HttpPost("accept/qr")]
        public async Task<IActionResult> AcceptByQr([FromBody] AcceptByQrDto dto)
        {
            var auth0Id =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst("sub")?.Value;

            if (auth0Id is null)
                return Unauthorized("Auth0Id (sub) not found in token.");

            var result = await _associationService.AcceptByQrAsync(dto.QrToken, auth0Id);
            return Ok(result);
        }
    }
}
