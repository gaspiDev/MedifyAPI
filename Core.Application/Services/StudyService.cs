using AutoMapper;
using Core.Application.DTOs.StudyDTO;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.Repositories;

namespace Core.Application.Services
{
    public class StudyService : IStudyService
    {
        private readonly IStudyRepository _studyRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorPatientRepository _dpRepository;

        public StudyService(IStudyRepository studyRepository, IMapper mapper, IDoctorPatientRepository dpRepository)
        {
            _studyRepository = studyRepository;
            _mapper = mapper;
            _dpRepository = dpRepository;
        }

        public async Task<IEnumerable<StudyForViewDto>> ReadStudiesAsync()
        {
            var studies = await _studyRepository.ReadStudies();
            return _mapper.Map<IEnumerable<StudyForViewDto>>(studies);
        }

        public async Task<StudyForViewDto?> ReadByIdAsync(Guid id)
        {
            var study = await _studyRepository.ReadStudyById(id);
            return _mapper.Map<StudyForViewDto?>(study);
        }

        public async Task<IEnumerable<StudyForViewDto>?> ReadStudiesyByPatientId(Guid patientId)
        {
            try
            {
                var study = await _studyRepository.ReadStudiesByPatientId(patientId);
                if (study == null)
                {
                    return null;
                }
                else
                {
                    var studiesforView = _mapper.Map<IEnumerable<StudyForViewDto>>(study);
                    return studiesforView;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error", ex);
            }
        }

        public async Task<StudyForViewDto?> CreateAsync(StudyForCreationDto dto)
        {
            var dpExists = await _dpRepository.ReadExistingAssociationsAsync(dto.DoctorId, dto.PatientId);
            if (dpExists == null)
            {
                return null;
            }
            var study = _mapper.Map<Study>(dto);
            await _studyRepository.CreateAsync(study);
            var fullStudy = await _studyRepository.ReadStudyById(study.Id);
            return _mapper.Map<StudyForViewDto>(study);
        }

        public async Task<bool> UpdateAsync(Guid id, StudyForUpdateDto dto)
        {
            var study = await _studyRepository.ReadStudyById(id);
            if (study is null) return false;

            _mapper.Map(dto, study);

            await _studyRepository.UpdateAsync(study);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var study = await _studyRepository.ReadByIdAsync(id);
            if (study is null) return false;

            await _studyRepository.DeleteAsync(study);
            return true;
        }
    }
}
