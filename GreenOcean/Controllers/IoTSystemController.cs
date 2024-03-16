using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoTSystem = GreenOcean.Entities.IoTSystem;

namespace GreenOcean.Controllers;

[ApiController]
[Route("iotsystems")]
public class IoTSystemController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public IoTSystemController(DataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    [HttpGet("getSystem/{id}")]
    public async Task<ActionResult<IoTSystemDTO>> GetSystem(Guid id)
    {
        var system = await dataContext.IoTSystems.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be returned");
        }

        var systemDTO = mapper.Map<IoTSystem, IoTSystemDTO>(system);

        return Ok(systemDTO);
    }

    [HttpGet("getSystems/{id}")]
    public async Task<ActionResult<IoTSystem>> GetSystems(Guid id)
    {
        var systems = await dataContext.IoTSystems.Where(s => s.GreenhouseId == id).ToListAsync();
        if (systems == null)
        {
            return BadRequest("The system cannot be returned");
        }

        var systemDTOs = mapper.Map<IEnumerable<IoTSystem>, IEnumerable<IoTSystemDTO>>(systems);

        return Ok(systemDTOs);
    }

    [HttpPost("addsystem")]
    public async Task<IActionResult> AddSystem(IoTSystemDTO ioTSystemDTO)
    {

        var ioTSystem = mapper.Map<IoTSystemDTO, IoTSystem>(ioTSystemDTO);
        ioTSystem.Timestamp = DateTime.UtcNow.Date;
        try
        {
            await dataContext.IoTSystems.AddAsync(ioTSystem);
            await dataContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }

    [HttpPut("editSystem/{id}")]
    public async Task<IActionResult> EditSystem(IoTSystemDTO ioTsystemDTO, Guid id)
    {
        var system = await dataContext.IoTSystems.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be updated");
        }

        mapper.Map<IoTSystemDTO, IoTSystem>(ioTsystemDTO, system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }


    [HttpDelete("deleteSystem/{id}")]
    public async Task<IActionResult> DeleteSystem(Guid id)
    {
        var system = await dataContext.IoTSystems.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be removed");
        }

        dataContext.IoTSystems.Remove(system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}