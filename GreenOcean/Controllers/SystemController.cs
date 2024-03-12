using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoTSystem = GreenOcean.Entities.System;

namespace GreenOcean.Controllers;

[ApiController]
[Route("systems")]
public class SystemController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public SystemController(DataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    [HttpGet("getSystem/{id}")]
    public async Task<ActionResult<SystemDTO>> GetSystem(Guid id)
    {
        var system = await dataContext.Systems.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be returned");
        }

        var systemDTO = mapper.Map<IoTSystem, SystemDTO>(system);

        return Ok(systemDTO);
    }

    [HttpGet("getSystems/{id}")]
    public async Task<ActionResult<IoTSystem>> GetSystems(Guid id)
    {
        var systems = await dataContext.Systems.Where(s => s.GreenhouseId == id).ToListAsync();
        if (systems == null)
        {
            return BadRequest("The system cannot be returned");
        }

        var systemDTOs = mapper.Map<IEnumerable<IoTSystem>, IEnumerable<SystemDTO>>(systems);

        return Ok(systemDTOs);
    }

    [HttpPost("addsystem")]
    public async Task<IActionResult> AddSystem(SystemDTO systemDTO)
    {
        var system = mapper.Map<SystemDTO, IoTSystem>(systemDTO);
        await dataContext.Systems.AddAsync(system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("editSystem/{id}")]
    public async Task<IActionResult> EditSystem(SystemDTO systemDTO, Guid id)
    {
        var system = await dataContext.Systems.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be updated");
        }

        mapper.Map<SystemDTO, IoTSystem>(systemDTO, system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }


    [HttpDelete("deleteSystem/{id}")]
    public async Task<IActionResult> DeleteSystem(Guid id)
    {
        var system = await dataContext.Systems.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be removed");
        }

        dataContext.Systems.Remove(system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}