using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
[Authorize(Roles = "Member")]
[Route("sensorData")]
public class DataControlller : ControllerBase
{
    private readonly IDataService _dataService;

    public DataControlller(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id}/{timestamp}")]
    public async Task<IEnumerable<DataDTO>> GetAsync(Guid id, string timestamp)
    {
        var data = await _dataService.GetData(id, timestamp);
        return data;
    }

    [HttpDelete("{id}/{timestamp}")]
    public async Task<IActionResult> DeleteData(Guid id, string timestamp)
    {
        var response = await _dataService.DeleteData(id, timestamp);
        if (response ==  false)
        {
            return BadRequest("Data cannot be removed");
        }

        return Ok();
    }
}