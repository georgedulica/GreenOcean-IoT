using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("equipments")]
public class RegisteredEquipmentController : ControllerBase
{
    private readonly IRegisteredEquipmentService _registeredEquipmentService;

    public RegisteredEquipmentController(IRegisteredEquipmentService registeredEquipmentService)
    {
        _registeredEquipmentService = registeredEquipmentService;
    }

    [HttpPost("addRegisteredEquipment")]
    public async Task<IActionResult> AddSystem(RegisteredEquipmentDTO registeredEquipmentDTO)
    {
        var response = await _registeredEquipmentService.AddRegisteredEquipment(registeredEquipmentDTO);
        if (response == false)
        {
            return BadRequest("The system already exists");
        }

        return Ok();
    }
}
