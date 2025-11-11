using Core.Application.Interfaces;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientRepository;
        public PatientController(IPatientService patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ReadPatients()
        {
            var patients = await _patientRepository.ReadPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById([FromRoute] Guid id)
        {
            var patient = await _patientRepository.ReadById(id);
            return Ok(patient);
        }

        [HttpGet("/doctors")]
        public async Task<IActionResult> ReadDoctorsByPatient([FromQuery] Guid patientId)
        {
            var doctors = await _patientRepository.ReadDoctorsByPatient(patientId);
            return Ok(doctors);
        }
    
    }

}
