using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
[Authorize(Roles = "Member")]
[Route("plants")]
public class PlantsController : ControllerBase
{
    private readonly IPlantService _plantService;

    public PlantsController(IPlantService plantService)
    {
        _plantService = plantService;
    }

    [HttpGet("getPlant/{id}")]
    public async Task<ActionResult<PlantDTO>> GetPlant(Guid id)
    {
        var plant = await _plantService.GetPlant(id);
        if (plant == null)
        {
            return BadRequest("The plant cannot be returned");
        }

        return Ok(plant);
    }

    [HttpGet("getPlants/{id}")]
    public async Task<ActionResult<IEnumerable<PlantDTO>>> GetPlants(Guid id)
    {
        var plants = await _plantService.GetPlants(id);
        if (plants == null)
        {
            return BadRequest("The plants cannot be returned");
        }

        return Ok(plants);
    }

    [HttpPost("createPlant")]
    public async Task<IActionResult> AddPlant(PlantDTO plantDTO)
    {
        var reseponse = await _plantService.AddPlant(plantDTO);
        if (reseponse == false)
        {
            return BadRequest("The plant already exits");
        }

        return Ok();
    }

    [HttpPut("editPlant/{id}")]
    public async Task<IActionResult> EditPlant(Guid id, PlantDTO plantDTO)
    {
        var response = await _plantService.EditPlant(id, plantDTO);
        if (response == false)
        {
            return BadRequest("The plant already exists");
        }

        return Ok();
    }

    [HttpDelete("deletePlant/{id}")]
    public async Task<IActionResult> DeletePlant(Guid id)
    {
        var response = await _plantService.DeletePlant(id);
        if (response == false)
        {
            return BadRequest("The plant cannot be removed");
        }

        return Ok();
    }
}