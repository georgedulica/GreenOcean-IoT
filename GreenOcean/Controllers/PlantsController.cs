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

        var result = await photoService.AddPhoto(plantDTO.File);

        if (result.Error != null)
        {
            return BadRequest("This photo cannot be uploaded");
        }

        try
        {
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
                PhotoURL = result.SecureUrl.AbsoluteUri,
                PhotoId = result.PublicId,
                GreenhouseId = greenhouseId
            };

            await dataContext.Plants.AddAsync(plant);
            await dataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await photoService.DeletePhoto(result.PublicId);
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

        var deletingResult = await photoService.DeletePhoto(plant.PhotoId);
        if (deletingResult.Error != null)
        {
            return BadRequest("The plant cannot be edited");
        }

        var result = await photoService.AddPhoto(plantDTO.File);

        if (result.Error != null)
        {
            return BadRequest("This photo cannot be edited");
        }

        try
        {
            plant.Name = plantDTO.Name;
            plant.Soil = plantDTO.Soil;
            plant.Height = plantDTO.Height;
            plant.Type = plantDTO.Type;
            plant.MositureLevel = plantDTO.MositureLevel;
            plant.Humidity = plantDTO.Humidity;
            plant.MaxTemperature = plantDTO.MaxTemperature;
            plant.MinTemperature = plantDTO.MinTemperature;
            plant.PhotoURL = result.SecureUrl.AbsoluteUri;
            plant.PhotoId = result.PublicId;
            plant.GreenhouseId = greenhouseId;
            await dataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await photoService.DeletePhoto(result.PublicId);
        }

        return Ok();
    }

    [HttpDelete("deletephtoto/id")]
    public async Task<IActionResult> DeletePlant(string id)
    {
        if (!Guid.TryParse(id, out Guid plantId))
        {
            return BadRequest("Invalid id format");
        }

        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
        
        var deletingResult = await photoService.DeletePhoto(plant.PhotoId);
        if (deletingResult.Error != null)
        {
            return BadRequest("The plant cannot be edited");
        }

        dataContext.Plants.Remove(plant);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}