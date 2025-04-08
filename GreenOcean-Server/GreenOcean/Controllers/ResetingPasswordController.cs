using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
[Route("resetpassword")]
public class ResetingPasswordController : ControllerBase
{
    private readonly IResetingPasswordService _resetingPasswordService;

    public ResetingPasswordController(IResetingPasswordService resetingPasswordService)
    {
        _resetingPasswordService = resetingPasswordService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid?>> SendEmail(EmailDTO emailDTO)
    {
        var id = await _resetingPasswordService.GenerateCode(emailDTO);
        if (id == null)
        {
            return BadRequest("The code cannot be generated");
        }

        return id;
    }

    [HttpPost("confirmcode/{id}")]
    public async Task<ActionResult<ResetingPasswordToken>> ConfirmCode(Guid id, CodeDTO codeDTO)
    {
        var resetingPasswordToken = await _resetingPasswordService.ConfirmCode(id, codeDTO);
        if (resetingPasswordToken == null)
        {
            return BadRequest("The code is not valid");
        }

        return Ok(resetingPasswordToken);
    }    
    
    [HttpPost("confirmcode/changepassword/{id}")]
    public async Task<IActionResult> ChangePassword(Guid id, PasswordDTO passwordDTO)
    {
        var response = await _resetingPasswordService.ChangePassword(id, passwordDTO);
        if (response == false)
        {
            return BadRequest();
        }

        return Ok();
    }
}