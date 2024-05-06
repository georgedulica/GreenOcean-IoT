using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Data;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GreenOcean.Controllers;

[ApiController]
[Route("processes")]
public class ProcessController : ControllerBase
{
    private readonly IProcessService _processService;

    public ProcessController(IProcessService processService)
    {
        _processService = processService;
    }

    [HttpGet("getProcess/{id}")]
    public async Task<ActionResult<ProcessDTO>> GetProcess(Guid id)
    {
        var process = await _processService.GetProcess(id);
        if (process == null)
        {
            return BadRequest();
        }

        return Ok(process);
    }

    [HttpGet("getProcesses/{timestamp}/{greenhouseId}")]
    public async Task<ActionResult<ProcessDTO>> GetProcesses(Guid greenhouseId)
    {
        var processes = await _processService.GetProcesses(greenhouseId);
        if (processes == null)
        {
            return BadRequest();
        }

        return Ok(processes);
    }

    [HttpPost("createProcess")]
    public async Task<IActionResult> CreateProcess(ProcessDTO processDTO)
    {
        var process = mapper.Map<ProcessDTO, Process>(processDTO);
        await dataContext.AddAsync(process);
        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("editProcess/{id}")]
    public async Task<IActionResult> EditProcess(ProcessDTO processDTO, Guid id)
    {
        var process = await dataContext.Processes.FirstOrDefaultAsync(p => p.Id == id);
        if (process == null)
        {
            return BadRequest();
        }

        mapper.Map<ProcessDTO, Process>(processDTO, process);

        await dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("deleteProcess/{id}")]
    public async Task<IActionResult> DeleteProcess(Guid id)
    {
        var process = await dataContext.Processes.FirstOrDefaultAsync(p => p.Id == id);
        if (process == null)
        {
            return BadRequest();
        }

        dataContext.Remove(process);
        await dataContext.SaveChangesAsync();

        return Ok();
    }

    private IEnumerable<ProcessDTO> CompareTimestamp(IEnumerable<ProcessDTO> processes, string timestamp)
    {
        IList<ProcessDTO> processesToReturn = new List<ProcessDTO>();
        foreach (var process in processes)
        {
            var timestampFromDbString = process.Timestamp.ToString("yyyy-MM-dd");
            if (string.Equals(timestampFromDbString, timestamp, StringComparison.OrdinalIgnoreCase))
            {
                processesToReturn.Add(process);
            }
        }

        return processesToReturn;
    }
}