using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;


[ApiController]
[Authorize]
[Route("greenhouses")]
public class GreenhouseController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public GreenhouseController(DataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetGreenhouses(string username)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        var greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).ToListAsync();
        var greenhousesAsDTO = mapper.Map<IEnumerable<GreenhouseDTO>>(greenhouses);

        return Ok(greenhousesAsDTO);
    }

    [HttpGet("greenhouse/{id}")]
    public async Task<ActionResult<GreenhouseDTO>> GetGreenhouse(Guid id)
    {
        var greenhouse = await dataContext.Greenhouses.FirstOrDefaultAsync(g => g.Id == id);
        var greenhouseAsDTO = mapper.Map<GreenhouseDTO>(greenhouse);

        return Ok(greenhouseAsDTO);
    }

    [HttpPost("creategreenhouse/{username}")]
    public async Task<IActionResult> CreateGreenhouse(GreenhouseDTO greenhouseDTO, string username)
    {
        var existingGreenhouse = await ExistingGreenhouse(greenhouseDTO);
        if (existingGreenhouse == true)
        {
            return BadRequest();
        }

        if (greenhouseDTO.Name == null)
        {
            return BadRequest();
        }
        
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        var greenhouse = mapper.Map<GreenhouseDTO, Greenhouse>(greenhouseDTO);
        greenhouse.UserId = user.Id;

        await dataContext.Greenhouses.AddAsync(greenhouse);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPut("editgreenhouse/{id}")]
    public async Task<IActionResult> EditGreenhouse(GreenhouseDTO greenhouseDTO, Guid id)
    {
        var existingGreenhouse = await ExistingGreenhouse(greenhouseDTO);
        if (existingGreenhouse == true)
        {
            return BadRequest();
        }

        if (greenhouseDTO.Name == null)
        {
            return BadRequest();
        }
        
        var greenhouse = await dataContext.Greenhouses.FirstOrDefaultAsync(g => g.Id == id);
        if (greenhouse == null)
        {
            return BadRequest();
        }

        mapper.Map<GreenhouseDTO, Greenhouse>(greenhouseDTO, greenhouse);

        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("deletegreenhouse/{id}")]
    public async Task<ActionResult> DeleteGreenhouse(Guid id)
    {
        var greenhouse = await dataContext.Greenhouses.FirstOrDefaultAsync(g => g.Id == id);
        if (greenhouse == null)
        {
            return BadRequest();
        }

        dataContext.Remove(greenhouse);
        await dataContext.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> ExistingGreenhouse(GreenhouseDTO greenhouseDTO)
    {
        var existingGreenhouse = await dataContext.Greenhouses.AnyAsync(g => string.Equals(g.Name, greenhouseDTO.Name) &&
            string.Equals(g.Street, greenhouseDTO.Street) && g.Number == greenhouseDTO.Number && string.Equals(g.City, greenhouseDTO.City));
        return existingGreenhouse;
    }
}