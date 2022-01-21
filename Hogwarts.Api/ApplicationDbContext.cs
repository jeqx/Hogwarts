using Hogwarts.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Api;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base (options)
    {
        
    }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<ClassRoom> ClassRooms { get; set; }

    
}