using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Hogwarts.Api.DTOs;

namespace Hogwarts.Api.Controllers;

[ApiController]
[Route("api/candidates")]
public class CandidatesController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ApplicationDbContext context;

    public CandidatesController(ApplicationDbContext context, IMapper mapper) 
    {
        this.context = context;
        this.mapper = mapper;

    }

    /// <summary>
    /// Gets All the candidates of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<CandidateDTO>>> Get()
    => mapper.Map<List<CandidateDTO>>(await context.Candidates.ToListAsync());

    /// <summary>
    /// Gets one the candidate of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetCandidate")]
    public async Task<ActionResult<CandidateDTO>> Get (int id)
    {
        var entidad = await context.Candidates.FirstOrDefaultAsync(x=>x.Id== id);
        if (entidad == null) return NotFound();

        return mapper.Map<CandidateDTO>(entidad);
    }
    
    /// <summary>
    /// Creates a new candidate to Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post ([FromForm] CandidateCreationDTO candidateCreationDTO)
    {
        var entidad = mapper.Map<Candidate>(candidateCreationDTO);
        
        context.Add(entidad);

        await context.SaveChangesAsync();
        
        var candidateDTO = mapper.Map<CandidateDTO>(entidad);
        
        return new CreatedAtRouteResult("GetCandidate", new{id = candidateDTO.Id}, candidateDTO);
    }

    /// <summary>
    /// Modifies an existing the candidate of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Put (int id, [FromForm] CandidateCreationDTO candidateCreationDTO)
    {
        var candidateDB= await context.Candidates.FirstOrDefaultAsync(x=>x.Id==id);

        if(candidateDB is null) return NotFound();

        candidateDB=mapper.Map(candidateCreationDTO,candidateDB);

        await context.SaveChangesAsync();

        return NoContent();

    }

    /// <summary>
    /// Remove a candidate of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete (int id)
    {
        var existe = await context.Candidates.AnyAsync(x=>x.Id == id);

        if(!existe) return NotFound();

        context.Remove(new Candidate(){Id=id});
        await context.SaveChangesAsync();

        return NoContent();
    }

}