using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly ICreatingUserService _creatingUserService;

    public UserController(ICreatingUserService creatingUserService)
    {
        _creatingUserService = creatingUserService;
    }

    [HttpGet("getUserRoles")]
    public IEnumerable<string> GetRoles()
    {
        var roles = Enum.GetValues(typeof(UserRole))
                        .Cast<UserRole>()
                        .Select(role => role.ToString()).ToList();
        return roles;
    }

    [HttpPost("createUser")]
    public async Task<ActionResult> CreateUser(UserDTO userDTO)
    {
        var response = await _creatingUserService.CreateUser(userDTO);
        if (response == false)
        {
            return BadRequest("This email already exists");
        }

        return Ok();
    }
}