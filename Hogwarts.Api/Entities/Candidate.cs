namespace Hogwarts.Api.Entities;

public class Candidate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public ulong Dni { get; set; }
    public uint Age { get; set; }
    public House YouAspire { get; set; }
}