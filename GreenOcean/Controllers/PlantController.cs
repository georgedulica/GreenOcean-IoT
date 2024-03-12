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

    [HttpGet("getplant/{id}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetPlant(Guid id)
    {
        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == id);
        var plantAsDTO = mapper.Map<Plant, PlantDTO>(plant);

        return Ok(plantAsDTO);
    }

    [HttpGet("getplants/{id}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetPlants(Guid id)
    {
        var plants = await dataContext.Plants.Where(g => g.GreenhouseId == id).ToListAsync();
        var plantsAsDTO = mapper.Map<IEnumerable<Plant>, IEnumerable<PlantDTO>>(plants);

        return Ok(plantsAsDTO);
    }

    [HttpPost("createplant")]
    public async Task<IActionResult> CreatePlant(PlantDTO plantDTO)
    {
        if (plantDTO.Name == null)
        {
            return BadRequest();
        }

        var plant = mapper.Map<PlantDTO, Plant>(plantDTO);

        plant.PhotoId = config.Value.PublicId;
        plant.PhotoURL = config.Value.URL;

        await dataContext.Plants.AddAsync(plant);
        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("editplant/{id}")]
    public async Task<IActionResult> EditPlant(PlantDTO plantDTO, Guid id)
    {
        if (plantDTO.Name == null)
        {
            return BadRequest();
        }

        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == id);
        if (plant == null)
        {
            return BadRequest("Invalid id");
        }

        mapper.Map<PlantDTO, Plant>(plantDTO, plant);
        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("deleteplant/{id}")]
    public async Task<IActionResult> DeletePlant(Guid id)
    {
        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == id);
        if (plant == null)
        {
            return BadRequest("Invalid id");
        }

        var deletingResult = await photoService.DeletePhoto(plant.PhotoId);
        if (deletingResult.Error != null)
        {
            return BadRequest("The plant cannot be deleted");
        }

        try
        {
            dataContext.Remove(plant);
            await dataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            plant.PhotoURL = config.Value.URL;
            plant.PhotoId = config.Value.PublicId;
            await dataContext.SaveChangesAsync();

            Console.WriteLine(ex);
            return BadRequest("The plant cannot be deleted");
        }

        return Ok();
    }
}