using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

public class PlantsController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;
    private readonly IPhotoService photoService;

    public PlantsController(DataContext dataContext, IMapper mapper, IPhotoService photoService)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        this.photoService = photoService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetPlants(string id)
    {
        if (!Guid.TryParse(id, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        var plants = await dataContext.Plants.Where(g => g.GreenhouseId == greenhouseId).ToListAsync();
        var plantsAsDTO = mapper.Map<IEnumerable<PlantDTO>>(plants);

        return Ok(plantsAsDTO);
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

        var result = await photoService.AddPhotoAsync(plantDTO.file);

        if (result.Error != null)
        {
            return BadRequest("This photo cannot be uploaded");
        }

        var plant = new Plant
        {
            Name = plantDTO.Name,
            Soil = plantDTO.Soil,
            Height = plantDTO.Height,
            Type = plantDTO.Type,
            MaxTemperature = plantDTO.MaxTemperature,
            MinTemperature = plantDTO.MinTemperature,
            PhotoURL =  result.SecureUrl.AbsoluteUri,
            PhotoId = result.PublicId,
            GreenhouseId = greenhouseId
        };

        await dataContext.Plants.AddAsync(plant);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}
