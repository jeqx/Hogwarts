using AutoMapper;
using Hogwarts.Api.DTOs;
using Hogwarts.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hogwarts.Api.Controllers;

[ApiController]
[Route("api/classroom")]
public class ClassRoomController : CustomBaseController
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public ClassRoomController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClassRoomDto>>> Get()
        => await Get<ClassRoom, ClassRoomDto>();

    [HttpGet("{id:int}", Name = "getClassRoom")]
    public async Task<ActionResult<ClassRoomDto>> Get(int id)
        => await Get<ClassRoom, ClassRoomDto>(id);

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] ClassRoomCreationDto classRoomCreationDto)
        => await Post<ClassRoomCreationDto, ClassRoom, ClassRoomDto>(classRoomCreationDto, "getClassRoom");

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ClassRoomCreationDto classRoomCreationDto)
        => await Put<ClassRoomCreationDto, ClassRoom>(id, classRoomCreationDto);

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
        => await Delete<ClassRoom>(id);
}