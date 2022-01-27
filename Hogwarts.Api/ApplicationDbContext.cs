using Hogwarts.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Api;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base (options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CandidateClassRoom>()
            .HasKey(ccr => new { ccr.ClassRoomId , ccr.CandidateId });
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseNpgsql("Host=127.0.0.1; Port=5432; Database=hogwarts; Username=jeq; Password=2018");

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<ClassRoom> ClassRooms { get; set; }
    
    public DbSet<CandidateClassRoom> CandidateClassRooms { get; set; }
    
}