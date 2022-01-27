using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Hogwarts.Api.DTOs;
using Hogwarts.Api.Entities;
using Microsoft.AspNetCore.Components.Web;

namespace Hogwarts.Api.Controllers;

[ApiController]
[Route("api/candidates")]
public class CandidatesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public CandidatesController(ApplicationDbContext context, IMapper mapper) 
    {
        this._context = context;
        this._mapper = mapper;

    }

    /// <summary>
    /// Gets All the candidates of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<CandidateDto>>> Get()
    => _mapper.Map<List<CandidateDto>>(await _context.Candidates.ToListAsync());

    /// <summary>
    /// Gets one the candidate of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetCandidate")]
    public async Task<ActionResult<CandidateDto>> Get (int id)
    {
        var entidad = await _context.Candidates.FirstOrDefaultAsync(x=>x.Id== id);
        if (entidad == null) return NotFound();

        return _mapper.Map<CandidateDto>(entidad);
    }
    
    /// <summary>
    /// Creates a new candidate to Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post ([FromForm] CandidateCreationDto candidateCreationDto)
    {
        if (candidateCreationDto.ClassRoomsIdsList is null) 
            return BadRequest("It is not possible to create a candidate without assigning a classroom");
      
        var classRoomIds = await _context.ClassRooms
            .Where(ccrDb => candidateCreationDto.ClassRoomsIdsList.Contains(ccrDb.Id))
            .Select(x => x.Id).ToListAsync();

        if (candidateCreationDto.ClassRoomsIdsList.Count != classRoomIds.Count) 
            return BadRequest("Classroom does not exist");
        var candidate = _mapper.Map<Candidate>(candidateCreationDto);
        _context.Add(candidate);

        await _context.SaveChangesAsync();
        
        var candidateDto = _mapper.Map<CandidateDto>(candidate);
        
        return new CreatedAtRouteResult("GetCandidate", new{id = candidate.Id}, candidateDto);

    }

    /// <summary>
    /// Modifies an existing the candidate of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Put (int id, [FromForm] CandidateCreationDto candidateCreationDto)
    {
        var candidateDb= await _context.Candidates.FirstOrDefaultAsync(x=>x.Id==id);

        if(candidateDb is null) return NotFound();

        candidateDb=_mapper.Map(candidateCreationDto,candidateDb);

        await _context.SaveChangesAsync();

        return NoContent();

    }

    /// <summary>
    /// Remove a candidate of Hogwarts
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete (int id)
    {
        var existe = await _context.Candidates.AnyAsync(x=>x.Id == id);

        if(!existe) return NotFound();

        _context.Remove(new Candidate(){Id=id});
        await _context.SaveChangesAsync();
        return NoContent();
    }

}