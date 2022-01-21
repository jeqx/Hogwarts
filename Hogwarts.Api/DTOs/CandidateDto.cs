using System.ComponentModel.DataAnnotations;

namespace Hogwarts.Api.DTOs;
public class CandidateDto : CandidateCreationDto
{
     public int Id { get; set; }

}