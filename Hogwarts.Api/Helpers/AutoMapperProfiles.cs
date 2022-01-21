using AutoMapper;
using Hogwarts.Api.DTOs;
using Hogwarts.Api.Entities;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Candidate, CandidateDto>().ReverseMap();
        CreateMap<CandidateCreationDto, Candidate>();
        CreateMap<ClassRoom, ClassRoomDto>().ReverseMap();
        CreateMap<ClassRoomCreationDto, ClassRoom>();
    }
}