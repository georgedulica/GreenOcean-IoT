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

    [HttpGet("greenhouse/{username}/{id}")]
    public async Task<ActionResult<GreenhouseDTO>> GetGreenhouse(string username, string id)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        if (!Guid.TryParse(id, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        var greenhouse = await dataContext.Greenhouses.FirstOrDefaultAsync(g => g.Id == greenhouseId && g.UserId == user.Id);
        var greenhouseAsDTO = mapper.Map<GreenhouseDTO>(greenhouse);

        return Ok(greenhouseAsDTO);
    }

    [HttpGet("orderbyname/{username}/{type}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> OrderByName(string username, string type)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        IEnumerable<Greenhouse> greenhouses = new List<Greenhouse>();

        if (string.Equals(type, "ascendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderBy(g => g.Name).ToListAsync();
        }

        if (string.Equals(type, "descendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderByDescending(g => g.Name).ToListAsync();
        }
        
        var greenhousesAsDTO = mapper.Map<IEnumerable<GreenhouseDTO>>(greenhouses);

        return Ok(greenhousesAsDTO);
    }    
    
    [HttpGet("orderbystreet/{username}/{type}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> OrderByStreet(string username, string type)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        IEnumerable<Greenhouse> greenhouses = new List<Greenhouse>();

        if (string.Equals(type, "ascendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderBy(g => g.Street).ToListAsync();
        }

        if (string.Equals(type, "descendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderByDescending(g => g.Street).ToListAsync();
        }
        
        var greenhousesAsDTO = mapper.Map<IEnumerable<GreenhouseDTO>>(greenhouses);

        return Ok(greenhousesAsDTO);
    }    
    
    [HttpGet("orderbystreetnumber/{username}/{type}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> OrderByNumber(string username, string type)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        IEnumerable<Greenhouse> greenhouses = new List<Greenhouse>();

        if (string.Equals(type, "ascendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderBy(g => g.Number).ToListAsync();
        }

        if (string.Equals(type, "descendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderByDescending(g => g.Number).ToListAsync();
        }
        
        var greenhousesAsDTO = mapper.Map<IEnumerable<GreenhouseDTO>>(greenhouses);

        return Ok(greenhousesAsDTO);
    }    
    
    [HttpGet("orderbycity/{username}/{type}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> OrderByCity(string username, string type)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        IEnumerable<Greenhouse> greenhouses = new List<Greenhouse>();

        if (string.Equals(type, "ascendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderBy(g => g.City).ToListAsync();
        }

        if (string.Equals(type, "descendent"))
        {
            greenhouses = await dataContext.Greenhouses.Where(g => g.UserId == user.Id).OrderByDescending(g => g.City).ToListAsync();
        }
        
        var greenhousesAsDTO = mapper.Map<IEnumerable<GreenhouseDTO>>(greenhouses);

        return Ok(greenhousesAsDTO);
    }

    [HttpPost("creategreenhouse/{username}")]
    public async Task<IActionResult> CreateGreenhouse(GreenhouseDTO greenhouseDTO, string username)
    {
        if (greenhouseDTO.Name == null)
        {
            return BadRequest();
        }
        
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        var greenhouse = new Greenhouse
        {
            Name = greenhouseDTO.Name,
            Street = greenhouseDTO.Street,
            Number = greenhouseDTO.Number,
            City = greenhouseDTO.City,
            UserId = user.Id
        };

        await dataContext.Greenhouses.AddAsync(greenhouse);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPut("editgreenhouse/{username}/{id}")]
    public async Task<IActionResult> EditGreenhouse(GreenhouseDTO greenhouseDTO, string username, string id)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        if (!Guid.TryParse(id, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        if (greenhouseDTO.Name == null)
        {
            return BadRequest();
        }
        
        var greenhouse = await dataContext.Greenhouses.FirstOrDefaultAsync(g => g.Id == greenhouseId && g.UserId == user.Id);
        if (greenhouse == null)
        {
            return BadRequest();
        }

        greenhouse.Name = greenhouseDTO.Name;
        greenhouse.Street = greenhouseDTO.Street;
        greenhouse.Street = greenhouseDTO.Street;
        greenhouse.City = greenhouseDTO.City;

        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("deletegreenhouse/{username}/{id}")]
    public async Task<ActionResult> DeleteGreenhouse(string username, string id)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return BadRequest();
        }

        if (!Guid.TryParse(id, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        var greenhouse = await dataContext.Greenhouses.FirstOrDefaultAsync(g => g.Id == greenhouseId && g.UserId == user.Id);
        if (greenhouse == null)
        {
            return BadRequest();
        }

        dataContext.Remove(greenhouse);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}