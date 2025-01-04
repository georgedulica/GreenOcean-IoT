using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
[Authorize(Roles = "Member")]
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
    public async Task<ActionResult<IEnumerable<ProcessDTO>>> GetProcesses(Guid greenhouseId)
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

    [HttpGet("ProcessStatuses")]
    public async Task<ActionResult<IEnumerable<ProcessStatus>>> GetProcessStatuses()
    {
        var processStatuses = LoadProcessStatuses();
        return Ok(processStatuses);
    }

    private IEnumerable<ProcessStatus> LoadProcessStatuses()
    {
        var processStatuses = new List<ProcessStatus>
        {
            new ProcessStatus { Status = "In Progress" },
            new ProcessStatus { Status = "Canceled" },
            new ProcessStatus { Status = "Completed" }
        };

        return processStatuses;
    }

}