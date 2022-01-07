using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base (options)
    {
        
    }

    public DbSet<Candidate> Candidates {get; set;}

    
}