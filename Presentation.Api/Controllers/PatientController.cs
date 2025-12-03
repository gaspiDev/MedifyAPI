using Core.Application.DTOs.PatientDTO;
using Core.Application.DTOs.User;
using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Domain.Entities;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService= patientService;
        }

        [HttpGet]
        public async Task<IActionResult> ReadPatients()
        {
            var patients = await _patientService.ReadPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById([FromRoute] Guid id)
        {
            var patient = await _patientService.ReadById(id);
            return Ok(patient);
        }

        [HttpGet("/doctors")]
        public async Task<IActionResult> ReadDoctorsByPatient([FromQuery] Guid patientId)
        {
            var doctors = await _patientService.ReadDoctorsByPatient(patientId);
            return Ok(doctors);
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] PatientForCreationDto patient)
        {
            try
            {
                var id = await _patientService.CreatePatientAsync(patient);

                return Ok(new { message = "User created successfully", Id = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user: {ex.Message}");
            }
        }

        [HttpPost("/Patient/SysAdmin")]
        public async Task<IActionResult> CreatePatientAsSysAdmin([FromBody] PatientForCreationDto patient)
        {
            try
            {
                var id = await _patientService.CreatePatientAsync(patient);

                return Ok(new { message = "User created successfully", Id = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user: {ex.Message}");
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PatientForUpdateDto dto)
        {
            var result = await _patientService.UpdatePatientAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("Patient/{id}")]
        public async Task<IActionResult> DeletePatientAsync([FromRoute] Guid id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }



        }

}
