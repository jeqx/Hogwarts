using Hogwarts.Api.Enums;

namespace Hogwarts.Api.Entities;

public class ClassRoom : IId
{
    public int Id { get; set; }

    public string Name { get; set; }

    public TypeClassRoom Type { get; set; }

    public int WeeklyHours { get; set; }
  
}