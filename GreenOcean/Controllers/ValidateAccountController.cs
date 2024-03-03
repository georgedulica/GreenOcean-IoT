using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Interfaces;
using GreenOcean.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

[ApiController]
[Route("validateaccount")]
public class ValidateAccountController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly ITokenService tokenService;
    private readonly ISettingPassword settingPassword;

    public ValidateAccountController(DataContext dataContext, ISettingPassword settingPassword, ITokenService tokenService)
    {
        this.dataContext = dataContext;
        this.settingPassword = settingPassword;
        this.tokenService = tokenService;
    }

    [HttpPost("confirmcode/{id}")]
    public async Task<IActionResult> ValidateAccount(CodeDTO codeDTO, string id)
    {
        if (!Guid.TryParse(id, out Guid userId))
        {
            return BadRequest("Invalid id format");
        }

        var code = await dataContext.Codes.FirstOrDefaultAsync(c => c.UserId == userId);
        if (code.GeneratedCode != codeDTO.Code)
        {
            return BadRequest("The code is invalid");
        }

        dataContext.Codes.Remove(code);
        await dataContext.SaveChangesAsync();

        var validationToken = new AccountValidationToken
        {
            Name = "validate",
            Token = tokenService.CreateToken(userId.ToString())
        };

        return Ok(validationToken);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ValidateAccount(ValidateAccountDTO validateAccountDTO, string id)
    {
        if (!Guid.TryParse(id, out Guid userId))
        {
            return BadRequest("Invalid id format");
        }

        if (!string.Equals(validateAccountDTO.Password, validateAccountDTO.ConfirmedPassword))
        {
            return BadRequest("Passwords does not match");
        }

        var existingUsername = dataContext.Users.Any(u => string.Equals(validateAccountDTO.Username, u.Username));
        if (existingUsername == true)
        {
            return BadRequest("Username exists");
        }

        var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        var hash = settingPassword.EncryptPassword(validateAccountDTO.Password, out var salt);
        
        user.Username = validateAccountDTO.Username;
        user.Password = hash;
        user.Salt = salt;

        await dataContext.SaveChangesAsync();
        
        return Ok();
    }

    private bool UsernameExists(string username)
    {
        var user = dataContext.Users.Any(u => string.Equals(username, u.Username));
        if (user == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
};