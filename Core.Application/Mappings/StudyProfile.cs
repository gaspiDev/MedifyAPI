using AutoMapper;
using Core.Application.DTOs.StudyDTO;
using Core.Domain.Entities;

public class StudyProfile : Profile
{
    public StudyProfile()
    {
        CreateMap<Study, StudyForViewDto>()
            .ForMember(dest => dest.DoctorName,
                       opt => opt.MapFrom(src => src.Doctor.FirstName))
            .ForMember(dest => dest.PatientName,
                       opt => opt.MapFrom(src => src.Patient.FirstName));

        CreateMap<StudyForCreationDto, Study>();

        CreateMap<StudyForUpdateDto, Study>()
            .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));
    }
}
