using AutoMapper;
using Hogwarts.Api.DTOs;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Candidate, CandidateDTO>().ReverseMap();
        CreateMap<CandidateCreationDTO, Candidate>();
    }
}