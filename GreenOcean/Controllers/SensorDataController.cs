using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data;
using GreenOcean.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

[ApiController]
[Route("sensorData")]
public class SensorDataController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;
    
    public SensorDataController(DataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    [HttpGet("getdata/{timestamp}/{id}")]
    public async Task<IEnumerable<DataDTO>> GetData(DateTime timestamp, Guid id)
    {
        var date = timestamp.ToString("yyyy-MM-dd");

        var data = await dataContext.SensorData
            .Where(d => d.EquipmentId == id)
            .ToListAsync();

        var dataDTO = mapper.Map<IEnumerable<SensorData>, IEnumerable<DataDTO>>(data);
        var dataToReturn = CompareTimestamp(dataDTO, date);
        return dataToReturn;
    }

    [HttpDelete("deletedata/{timestamp}")]
    public async Task<IActionResult> DeleteData(string timestamp)
    {
        var data = await dataContext.SensorData.FirstOrDefaultAsync(d => string.Equals(d.Timestamp, timestamp));
        if (data == null)
        {
            return BadRequest("Data cannot be removed");
        }

        dataContext.Remove(data);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
    private IEnumerable<DataDTO> CompareTimestamp(IEnumerable<DataDTO> data, string timestamp)
    {
        IList<DataDTO> dataToReturn = new List<DataDTO>();
        foreach (var item in data)
        {
            var timestampFromDbString = DateTime.Parse(item.Timestamp).ToString("yyyy-MM-dd");
            if (string.Equals(timestampFromDbString, timestamp, StringComparison.OrdinalIgnoreCase))
            {
                dataToReturn.Add(item);
            }
        }

        return dataToReturn;
    }
}