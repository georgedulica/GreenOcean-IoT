using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

[ApiController]
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

    [HttpPut("/creategreenhouse/{username}")]
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

    [HttpGet("{username}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetGreenHouse(string username)
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
}