using Core.Application.DTOs.AppointmentDTO;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentRepository;
        public AppointmentController(IAppointmentService appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ReadAppointments()
        {
            var appointments = await _appointmentRepository.ReadAppointments();
            return Ok(appointments);
        }

        [HttpGet("Doctor/{Doctorid}")]
        public async Task<IActionResult> ReadByDoctorId([FromRoute] Guid Doctorid)
        {
            var appointment = await _appointmentRepository.ReadAppointmentsByDoctor(Doctorid);
            return Ok(appointment);
        }

        [HttpGet("Patient/{Patientid}")]
        public async Task<IActionResult> ReadByPatientId([FromRoute] Guid Patientid)
        {
            var appointment = await _appointmentRepository.ReadAppointmentsByPatient(Patientid);
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentForCreationDto appointment)
        {
            var createdAppointment = await _appointmentRepository.CreateAppointment(appointment);
            return Ok(createdAppointment);
        }
    }
    }
