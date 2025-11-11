using Core.Application.Interfaces;
using Core.Application.Services;
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

        [HttpGet("/Patients")]
        public async Task<IActionResult> ReadPatientsByDoctor([FromQuery] Guid doctorId)
        {
            var patients = await _doctorService.ReadPatientsByDoctor(doctorId);
            return Ok(patients);
        }

        
    }
}
