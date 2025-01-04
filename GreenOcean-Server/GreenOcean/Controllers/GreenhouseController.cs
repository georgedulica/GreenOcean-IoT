using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;


[ApiController]
[Authorize(Roles = "Member")]
[Route("greenhouses")]
public class GreenhouseController : ControllerBase
{
    private readonly IGreenhouseService _greenhouseService;

    public GreenhouseController(IGreenhouseService greenhouseService)
    {
        _greenhouseService = greenhouseService;
    }

    [HttpGet("getGreenhouses/{username}")]
    public async Task<ActionResult<IEnumerable<GreenhouseDTO>>> GetGreenhouses(string username)
    {
        var greenhouses = await _greenhouseService.GetGreenhouses(username);
        if (greenhouses == null)
        {
            return BadRequest("The greenhouses cannot be returned");

        }

        return Ok(greenhouses);
    }

    [HttpGet("getGreenhouse/{id}")]
    public async Task<ActionResult<GreenhouseDTO>> GetGreenhouse(Guid id)
    {
        var greenhouse = await _greenhouseService.GetGreenhouse(id);
        if (greenhouse == null)
        {
            return BadRequest("The greenhouse cannot be returned");
        }

        return Ok(greenhouse);
    }

    [HttpPost("addGreenhouse/{username}")]
    public async Task<IActionResult> AddGreenhouse(string username, GreenhouseDTO greenhouseDTO)
    {
        var response = await _greenhouseService.AddGreenhouse(username, greenhouseDTO);
        if (response == false)
        {
            return BadRequest("This greenhouse already exits");
        }

        return Ok();
    }
    
    [HttpPut("editGreenhouse/{id}")]
    public async Task<IActionResult> EditGreenhouse(Guid id, GreenhouseDTO greenhouseDTO)
    {
        var response = await _greenhouseService.EditGreenhouse(id, greenhouseDTO);
        if (response == false)
        {
            return BadRequest("This greenhouse already exits");
        }

        return Ok();
    }

    [HttpDelete("deleteGreenhouse/{id}")]
    public async Task<ActionResult> DeleteGreenhouse(Guid id)
    {
        var response = await _greenhouseService.DeleteGreenhouse(id);
        if (response == false)
        {
            return BadRequest("The greenhouse cannot be removed");
        }

        return Ok();
    }
}