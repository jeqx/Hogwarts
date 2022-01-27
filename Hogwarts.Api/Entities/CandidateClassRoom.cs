namespace Hogwarts.Api.Entities;

public class CandidateClassRoom
{
    public int CandidateId { get; set; }
    public int ClassRoomId { get; set; }

    public Candidate Candidate { get; set; }
    public ClassRoom ClassRoom { get; set; }
}