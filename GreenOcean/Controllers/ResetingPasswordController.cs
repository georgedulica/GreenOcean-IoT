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
    public async Task<ActionResult<ResetingPasswordToken?>> SendEmail(EmailDTO emailDTO)
    {
        var resetingPasswordToken = await _resetingPasswordService.GenerateCode(emailDTO);
        return resetingPasswordToken;
    }

    [HttpPost("confirmcode/{id}")]
    public async Task<IActionResult> ConfirmCode(Guid id, CodeDTO codeDTO)
    {
        var response = await _resetingPasswordService.ConfirmCode(id, codeDTO);
        if (response == false)
        {
            return BadRequest("The code is not valid");
        }

        return Ok();
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