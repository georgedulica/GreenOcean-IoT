using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data;
using GreenOcean.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<EquipmentDTO>> GetSystem(Guid id)
    {
        var system = await dataContext.Equipments.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be returned");
        }

        var systemDTO = mapper.Map<Equipment, EquipmentDTO>(system);

        return Ok(systemDTO);
    }

    [HttpGet("getSystems/{id}")]
    public async Task<ActionResult<Equipment>> GetSystems(Guid id)
    {
        var systems = await dataContext.Equipments.Where(s => s.GreenhouseId == id).ToListAsync();
        if (systems == null)
        {
            return BadRequest("The system cannot be returned");
        }

        var systemDTOs = mapper.Map<IEnumerable<Equipment>, IEnumerable<EquipmentDTO>>(systems);

        return Ok(systemDTOs);
    }

    [HttpPost("addsystem")]
    public async Task<IActionResult> AddSystem(EquipmentDTO ioTSystemDTO)
    {

        var ioTSystem = mapper.Map<EquipmentDTO, Equipment>(ioTSystemDTO);
        ioTSystem.Timestamp = DateTime.UtcNow.Date;
        try
        {
            await dataContext.Equipments.AddAsync(ioTSystem);
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
    public async Task<IActionResult> EditSystem(EquipmentDTO ioTsystemDTO, Guid id)
    {
        var system = await dataContext.Equipments.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be updated");
        }

        mapper.Map<EquipmentDTO, Equipment>(ioTsystemDTO, system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }


    [HttpDelete("deleteSystem/{id}")]
    public async Task<IActionResult> DeleteSystem(Guid id)
    {
        var system = await dataContext.Equipments.FirstOrDefaultAsync(s => s.Id == id);
        if (system == null)
        {
            return BadRequest("The system cannot be removed");
        }

        dataContext.Equipments.Remove(system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}