using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using GreenOcean.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GreenOcean.Controllers;

[ApiController]
[Authorize]
[Route("plants")]
public class PlantsController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;
    private readonly IPhotoService photoService;
    private readonly IOptions<BasicPhotoSettings> config;

    public PlantsController(DataContext dataContext, IMapper mapper, IPhotoService photoService, IOptions<BasicPhotoSettings> config)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        this.photoService = photoService;
        this.config = config;
    }

    [HttpGet("getplants/{id}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetPlants(string id)
    {
        if (!Guid.TryParse(id, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        var plants = await dataContext.Plants.Where(g => g.GreenhouseId == greenhouseId).ToListAsync();
        var plantsAsDTO = mapper.Map<IEnumerable<Plant>, IEnumerable<PlantDTO>>(plants);

        return Ok(plantsAsDTO);
    }

    [HttpGet("getplant/{id}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetPlant(string id)
    {
        if (!Guid.TryParse(id, out Guid plantId))
        {
            return BadRequest("Invalid id format");
        }

        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
        var plantAsDTO = mapper.Map<Plant, PlantDTO>(plant);

        return Ok(plantAsDTO);
    }

    [HttpPost("createplant")]
    public async Task<IActionResult> CreatePlant(PlantDTO plantDTO)
    {
        if (!Guid.TryParse(plantDTO.GreenhouseId, out Guid greenhouseId))
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
            MositureLevel = plantDTO.MositureLevel,
            Humidity = plantDTO.Humidity,
            MaxTemperature = plantDTO.MaxTemperature,
            MinTemperature = plantDTO.MinTemperature,
            GreenhouseId = greenhouseId,
            PhotoId = config.Value.PublicId,
            PhotoURL = config.Value.URL
        };

        await dataContext.Plants.AddAsync(plant);
        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("plantphoto")]
    public async Task<IActionResult> SetPhoto(IFormFile file)
    {

        var result = await photoService.AddPhoto(file);

        if (result.Error != null)
        {
            return BadRequest("This photo cannot be uploaded");
        }

        return Ok();
    }

    [HttpPut("editplant/{id}")]
    public async Task<IActionResult> EditPlant(PlantDTO plantDTO, string id)
    {
        if (!Guid.TryParse(id, out Guid plantId))
        {
            return BadRequest("Invalid id format");
        }

        if (!Guid.TryParse(plantDTO.GreenhouseId, out Guid greenhouseId))
        {
            return BadRequest("Invalid id format");
        }

        if (plantDTO.Name == null)
        {
            return BadRequest();
        }

        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
        if (plant == null)
        {
            return BadRequest("Invalid id");
        }

        plant.Name = plantDTO.Name;
        plant.Soil = plantDTO.Soil;
        plant.Height = plantDTO.Height;
        plant.Type = plantDTO.Type;
        plant.MositureLevel = plantDTO.MositureLevel;
        plant.Humidity = plantDTO.Humidity;
        plant.MaxTemperature = plantDTO.MaxTemperature;
        plant.MinTemperature = plantDTO.MinTemperature;
        plant.GreenhouseId = greenhouseId;

        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("deleteplant/{id}")]
    public async Task<IActionResult> DeletePlant(string id)
    {
        if (!Guid.TryParse(id, out Guid plantId))
        {
            return BadRequest("Invalid id format");
        }

        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
        if (plant == null)
        {
            return BadRequest("Invalid id");
        }

        dataContext.Remove(plant);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}