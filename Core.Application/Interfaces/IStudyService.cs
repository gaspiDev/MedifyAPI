using Core.Application.DTOs.StudyDTO;

public interface IStudyService
{
    Task<StudyForViewDto?> CreateAsync(StudyForCreationDto dto);
    Task<IEnumerable<StudyForViewDto>> ReadStudiesAsync();
    Task<StudyForViewDto?> ReadByIdAsync(Guid id);
    Task<IEnumerable<StudyForViewDto>?> ReadStudiesyByPatientId(Guid patientId);
    Task<bool> UpdateAsync(Guid id, StudyForUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}