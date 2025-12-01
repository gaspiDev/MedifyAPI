using Core.Application.DTOs.AssociationDTO;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DoctorPatientController : Controller
    {
        private readonly IDoctorPatientService _doctorPatientService;
        public DoctorPatientController(IDoctorPatientService doctorPatientService)
        {
            _doctorPatientService = doctorPatientService;
        }

        [HttpGet("Doctor/{doctorId}")]
        public async Task<IActionResult> ReadPatientsNotAssociatedWithDoctorAsync(Guid doctorId)
        {
            var patient = await _doctorPatientService.ReadPatientsNotAssociatedWithDoctorAsync(doctorId);
            return Ok(patient);
        }

        [HttpPost("associate")]
        public async Task<IActionResult> AssociateDoctorPatient([FromBody] AssociationAsSysAdminDto dto)
        {
            var result = await _doctorPatientService.CreateAssociationAsync(dto);
            if (result != null)
            {
                return Ok(new { Message = "Doctor and patient associated successfully." });
            }
            return BadRequest(new { Message = "Failed to associate doctor and patient." });
        }

        [HttpPost("disassociate/{associationId}")]
        public async Task<IActionResult> DisassociateDoctorPatient(Guid associationId)
        {
            var result = await _doctorPatientService.UnassignAssociationAsync(associationId);
            if (result != null)
            {
                return Ok(new { Message = "Doctor and patient disassociated successfully." });
            }
            return BadRequest(new { Message = "Failed to disassociate doctor and patient." });
        }

        }

}
