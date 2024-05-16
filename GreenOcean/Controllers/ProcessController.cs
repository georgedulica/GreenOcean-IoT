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
            return BadRequest("The process cannot be returned");
        }

        return Ok(process);
    }

    [HttpGet("getProcesses/{greenhouseId}")]
    public async Task<ActionResult<ProcessDTO>> GetProcesses(Guid greenhouseId)
    {
        var processes = await _processService.GetProcesses(greenhouseId);
        if (processes == null)
        {
            return BadRequest("The processes cannot be returned");
        }

        return Ok(processes);
    }

    [HttpPost("createProcess")]
    public async Task<IActionResult> CreateProcess(ProcessDTO processDTO)
    {
        var response = await _processService.AddProcess(processDTO);
        if (response == false)
        {
            return BadRequest("The process already exits");
        }

        return Ok();
    }

    [HttpPut("editProcess/{id}")]
    public async Task<IActionResult> EditProcess(ProcessDTO processDTO, Guid id)
    {
        var response = await _processService.EditProcess(id, processDTO);
        if (response == false)
        {
            return BadRequest("The process already exits");
        }

        return Ok();
    }

    [HttpDelete("deleteProcess/{id}")]
    public async Task<IActionResult> DeleteProcess(Guid id)
    {
        var response = await _processService.DeleteProcess(id);
        if (response == false)
        {
            return BadRequest("The process already exits");
        }

        return Ok();
    }
}