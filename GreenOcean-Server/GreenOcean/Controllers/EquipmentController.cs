using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;


[ApiController]
[Authorize(Roles = "Member")]
[Route("iotsystems")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService, IMapper mapper)
    {
        _equipmentService = equipmentService;
    }

    [HttpGet("getSystem/{id}")]
    public async Task<ActionResult<EquipmentDTO>> GetSystem(Guid id)
    {
        var equipment = await _equipmentService.GetEquipment(id);
        if (equipment == null)
        {
            return BadRequest("The system cannot be returned");
        }

        return Ok(equipment);
    }

    [HttpGet("getSystems/{id}")]
    public async Task<ActionResult<Equipment>> GetSystems(Guid id)
    {
        var equipment = await _equipmentService.GetEquipments(id);
        if (equipment == null)
        {
            return BadRequest("The equipment cannot be returned");
        }

        return Ok(equipment);
    }

    [HttpPost("addsystem")]
    public async Task<IActionResult> AddSystem(EquipmentDTO equipmentDTO)
    {

        var response = await _equipmentService.AddEquipment(equipmentDTO);
        if (response == false)
        {
            return BadRequest("The equipment already exists");
        }

        return Ok();
    }

    [HttpPut("editSystem/{id}")]
    public async Task<IActionResult> EditSystem(Guid id, EquipmentDTO equipmentDTO)
    {
        var response = await _equipmentService.EditEquipment(id, equipmentDTO);
        if (response == false)
        {
            return BadRequest("The equipment already exists");
        }

        return Ok();
    }

    [HttpDelete("deleteSystem/{id}")]
    public async Task<IActionResult> DeleteSystem(Guid id)
    {
        var response = await _equipmentService.DeleteEquipment(id);
        if (response == false)
        {
            return BadRequest("The equipment cannot be removed");
        }

        return Ok();
    }
}