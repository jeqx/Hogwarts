using AutoMapper;
using Hogwarts.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Api.Controllers;

public class CustomBaseController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CustomBaseController( ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    /// <summary>
    /// Generic method to get all records
    /// </summary>
    /// <typeparam name="TEntity">Entity Class</typeparam>
    /// <typeparam name="TDto">Class that represents the DTO for the entity</typeparam>
    /// <returns></returns>
    protected async Task<List<TDto>> Get<TEntity, TDto>()
        where TEntity : class
    {
        var entities = await context.Set<TEntity>().AsNoTracking().ToListAsync();
        return mapper.Map<List<TDto>>(entities);
    }
    
    /// <summary>
    /// Generic method to obtain a record by id
    /// </summary>
    /// <param name="id">integer record number</param>
    /// <typeparam name="TEntity">Entity Class</typeparam>
    /// <typeparam name="TDto">Class that represents the DTO for the entity</typeparam>
    /// <returns></returns>
    protected async Task<ActionResult<TDto>> Get<TEntity, TDto>(int id) where TEntity : class, IId
    {
        var entity = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null) return NotFound();
        
        else return mapper.Map<TDto>(entity);
    }

    /// <summary>
    /// Generic method to add a new record
    /// </summary>
    /// <param name="creationDto">CreationDto object</param>
    /// <param name="namePath">name used in decorator as path</param>
    /// <typeparam name="TCreation">CreationDto Class</typeparam>
    /// <typeparam name="TEntity">Entity Class</typeparam>
    /// <typeparam name="TReader">Reading Class</typeparam>
    /// <returns></returns>
    protected async Task<ActionResult> Post<TCreation, TEntity, TReader>(TCreation creationDto, string namePath)
        where TEntity : class, IId
    {
        var entity = mapper.Map<TEntity>(creationDto);
        context.Add(entity);
        await context.SaveChangesAsync();
        var dtoReading = mapper.Map<TReader>(entity);
        return new CreatedAtRouteResult(namePath, new {id = entity.Id}, dtoReading);
    }

    /// <summary>
    /// Generic method to update record
    /// </summary>
    /// <param name="id">Integer record number</param>
    /// <param name="creationDto">CreationDto Object</param>
    /// <typeparam name="TCreation">Creation Class</typeparam>
    /// <typeparam name="TEntity">Entity Class</typeparam>
    /// <returns></returns>
    protected async Task<ActionResult> Put<TCreation, TEntity>(int id, TCreation creationDto) 
        where TEntity : class, IId
    {
        var entity = mapper.Map<TEntity>(creationDto);
        entity.Id = id;
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }
    
    /// <summary>
    /// Generic method to delete record
    /// </summary>
    /// <param name="id">  integer record number</param>
    /// <typeparam name="TEntity">Entity Class</typeparam>
    /// <returns></returns>
    protected async Task<ActionResult> Delete<TEntity>(int id)
        where TEntity : class, IId, new()
    {
        if (await context.Set<TEntity>().AnyAsync(x=> x.Id == id))
        {
            context.Remove(new TEntity() {Id = id});
            await context.SaveChangesAsync();
            return NoContent();
        }
        else return NotFound();
    }
    
}