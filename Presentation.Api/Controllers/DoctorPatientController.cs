using Core.Application.DTOs.AssociationDTO;
using Core.Application.Interfaces;
using Core.Application.Services;
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

        [HttpDelete("disassociate/{doctorId}")]
        public async Task<IActionResult> DisassociateDoctorPatient([FromRoute] Guid doctorId, [FromQuery] Guid patientId)
        {
            var result = await _doctorPatientService.UnassignAssociationAsync(doctorId, patientId);
            if (result == null)
            {
                return NotFound(new { Message = "Association not found." });
            }


            return NoContent();
        }

    }

}
