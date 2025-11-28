using Microsoft.AspNetCore.Mvc;
using Core.Application.Interfaces;
using Core.Application.DTOs.StudyDTO;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class StudyController : Controller
    {
        private readonly IStudyService _studyService;
        private readonly IStorageService _storageService;

        public StudyController(IStudyService studyService, IStorageService storageService)
        {
            _studyService = studyService;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllStudies()
        {
            var studies = await _studyService.ReadStudiesAsync();
            return Ok(studies);
        }
        [HttpGet("Study/{id}")]
        public async Task<IActionResult> ReadStudyById([FromRoute] Guid id) 
        {
            var study = await _studyService.ReadByIdAsync(id);
            if (study == null)
            {
                return NotFound();
            }
            return Ok(study);
        }

        [HttpGet("/Patient/{patientId}")]
        public async Task<IActionResult> ReadStudyByPatient([FromRoute]Guid patientId)
        {
            var studies = await _studyService.ReadStudiesyByPatientId(patientId);
            return Ok(studies);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> upload([FromForm] StudyUploadRequest request)
        {
            string? uploadedurl = null;

            if (request.File != null)
            {
                uploadedurl = await _storageService.UploadStudyFile(request.File);
            }

            var dto = new StudyForCreationDto
            {
                Type = request.Type,
                Notes = request.Notes,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                AppointmentId = request.AppointmentId,
                StudyDate = request.StudyDate,
                StudyUrl = uploadedurl,
                FileName = request.File?.FileName
            };

            var result = await _studyService.CreateAsync(dto);

            return Ok(result);
        }
    }
}
