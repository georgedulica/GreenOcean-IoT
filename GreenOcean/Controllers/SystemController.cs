using AutoMapper;
using GreenOcean.Data;
using GreenOcean.DTOs;
using Microsoft.AspNetCore.Mvc;
using RegisteredSystem = GreenOcean.Entities.System;

namespace GreenOcean.Controllers;

[ApiController]
[Route("systems")]
public class SystemController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public SystemController(DataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    [HttpPost("addsystem")]
    public async Task<IActionResult> AddSystem(SystemDTO systemDTO)
    {
        var system = mapper.Map<SystemDTO, RegisteredSystem>(systemDTO);
        await dataContext.Systems.AddAsync(system);
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}
