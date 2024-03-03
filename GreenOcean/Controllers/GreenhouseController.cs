using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

[ApiController]
[Route("greenhouse")]
public class GreenhouseController : ControllerBase
{
    private readonly DataContext dataContext;

    public GreenhouseController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPut("{username}")]
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
}