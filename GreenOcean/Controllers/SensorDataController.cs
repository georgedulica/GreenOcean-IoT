using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
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

    [HttpGet("getdata/{id}")]
    public async Task<IEnumerable<DataDTO>> GetData(Guid id)
    {
        var data = await dataContext.SensorData.Where(d => d.IoTSystemId == id).ToListAsync();
        var dataDTO = mapper.Map<IEnumerable<SensorData>, IEnumerable<DataDTO>>(data);

        return dataDTO;
    }
}