using AutoMapper;
using Hogwarts.Api.DTOs;
using Hogwarts.Api.Entities;

namespace Hogwarts.Api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Candidate, CandidateDto>().ReverseMap();
        CreateMap<CandidateCreationDto, Candidate>()
            .ForMember(c => c.CandidateClassRoomsList, options => options.MapFrom(ClassRoomListMap));
        CreateMap<ClassRoom, ClassRoomDto>().ReverseMap();
        CreateMap<ClassRoomCreationDto, ClassRoom>();
    }

    private List<CandidateClassRoom> ClassRoomListMap(CandidateCreationDto candidateCreationDto, Candidate candidate)
    {
        var result = new List<CandidateClassRoom>();
        
        if (candidateCreationDto.ClassRoomsIdsList is null) return result;
        
        foreach (var classRoomId in candidateCreationDto.ClassRoomsIdsList)
        {
            result.Add(new(){ ClassRoomId = classRoomId});
        }
        return result;
    }
}