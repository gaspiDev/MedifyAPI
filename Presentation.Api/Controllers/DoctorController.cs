using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;
using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]
        public async Task<IActionResult> ReadDoctors()
        {
            var doctors = await _doctorService.ReadDoctors();
            return Ok(doctors);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> ReadById([FromRoute] Guid id)
        {
            var doctor = await _doctorService.ReadById(id);
            return Ok(doctor);
        }

        [HttpGet("/Patients/{doctorId}")]
        public async Task<IActionResult> ReadPatientsByDoctor([FromRoute] Guid doctorId)
        {
            var patients = await _doctorService.ReadPatientsByDoctor(doctorId);
            return Ok(patients);
        }

        [HttpGet("/DoctorPatient/{doctorId}")]
        public async Task<IActionResult> ReadPatientsNotAssociatedByDoctorAsync([FromRoute] Guid doctorId)
        {
            var patients = await _doctorService.ReadPatientsNotAssociatedByDoctorAsync(doctorId);
            return Ok(patients);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] DoctorForCreationDto doctor)
        {
            try
            {
                var id = await _doctorService.CreateDoctorAsync(doctor);

                return Ok(new { message = "User created successfully", Id = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user: {ex.Message}");
            }
        }

        [HttpPost("/Doctor/SysAdmin")]
        public async Task<IActionResult> CreateDoctorAsSysAdmin([FromBody] DoctorForCreationDto doctor)
        {
            try
            {
                var id = await _doctorService.CreateDoctorAsync(doctor);

                return Ok(new { message = "User created successfully", Id = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user: {ex.Message}");
            }
        }

    }
}