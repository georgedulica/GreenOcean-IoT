using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

public class PlantsController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public PlantsController(DataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetGreenhouses(string id)
    {
        if (!Guid.TryParse(id, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        var plants = await dataContext.Plants.Where(g => g.GreenhouseId == greenhouseId).ToListAsync();
        return Ok(plants);
    }

    [HttpPost("creategreenhouse/{id}")]
    public async Task<IActionResult> CreateGreenhouse(PlantDTO plantDTO, string id)
    {
        if (!Guid.TryParse(id, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        if (plantDTO.Name == null)
        {
            return BadRequest();
        }

        var plant = new Plant
        {
            Name = plantDTO.Name,
            Soil = plantDTO.Soil,
            Height = plantDTO.Height,
            Type = plantDTO.Type,
            MaxTemperature = plantDTO.MaxTemperature,
            MinTemperature = plantDTO.MinTemperature,
            GreenhouseId = greenhouseId
        };

        await dataContext.Plants.AddAsync(plant);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}
