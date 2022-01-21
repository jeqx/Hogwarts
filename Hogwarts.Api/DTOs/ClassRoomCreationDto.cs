using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Hogwarts.Api.Enums;

namespace Hogwarts.Api.DTOs;

public class ClassRoomCreationDto
{
    [Required]
    [StringLength(30)]
    public string Name { get; set; }
    
    [Required]
    [Range(1,4,ErrorMessage = "4 hours Maximum" )]
    public uint WeeklyHours { get; set; }

    
    [Required]
    [EnumDataType(typeof(TypeClassRoom))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeClassRoom Type { get; set; }

}