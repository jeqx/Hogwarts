using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Hogwarts.Api.DTOs;

public class CandidateCreationDTO
{   [Required]
    [StringLength (20)]
    [RegularExpression("^[a-zA-Z]*$", ErrorMessage ="Only char")]
    public string Name { get; set; }

    [Required]
    [StringLength (20)]
    [RegularExpression("^[a-zA-Z]*$", ErrorMessage ="Only char")]
    public string Lastname { get; set; }

    [Required]
    [Range (0,9999999999, ErrorMessage ="Max 10 Dig")]

    public ulong Dni { get; set; }

    [Required]
    [Range (0,99, ErrorMessage ="Max 2 Dig")]
    public uint Age { get; set; }

    [Required]
    [EnumDataType(typeof(House))]    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public House YouAspire { get; set; }
}