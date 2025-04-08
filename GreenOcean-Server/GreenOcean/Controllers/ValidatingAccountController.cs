using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
[Route("validateaccount")]
public class ValidatingAccountController : ControllerBase
{
    private readonly IValidatingAccountService _validatingAccountService;

    public ValidatingAccountController(IValidatingAccountService validatingAccountService)
    {
        _validatingAccountService = validatingAccountService;
    }

    [HttpPost("confirmcode/{id}")]
    public async Task<IActionResult> ValidateAccount(CodeDTO codeDTO, Guid id)
    {
        var validatingAccountToken = await _validatingAccountService.CheckCode(id, codeDTO);
        if (validatingAccountToken == null)
        {
            return BadRequest("The code is not correct");
        }

        return Ok(validatingAccountToken);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ValidateAccount(AccountValidationDTO accountValidationDTO, Guid id)
    {
        var validatedAccount = await _validatingAccountService.ValidateAccount(id, accountValidationDTO);
        if (validatedAccount == false)
        {
            return BadRequest("The username already exists");
        }

        return Ok();
    }
};