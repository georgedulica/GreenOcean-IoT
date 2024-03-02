using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

[ApiController]
public class ValidateAccountController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly ISettingPassword settingPassword;

    public ValidateAccountController(DataContext dataContext, ISettingPassword settingPassword)
    {
        this.dataContext = dataContext;
        this.settingPassword = settingPassword;
    }

    [HttpPut("/validateaccount/{id}")]
    public async Task<IActionResult> ValidateAccount(string id, ValidateAccountDTO validateAccountDTO)
    {
        User account;
        Code code;
        int generatedCode;

        if (!Guid.TryParse(id, out Guid userId))
        {
            return BadRequest("Invalid id format");
        }

        var user = UsernameExists(validateAccountDTO.Username);
        if (user == false)
        {
            account = await dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        else
        {
            return BadRequest();
        }

        if (account != null)
        {
            (code, generatedCode) = await GetCode(userId);
        }
        else
        {
            return BadRequest();
        }

        if (generatedCode == validateAccountDTO.Code)
        {
            var hash = settingPassword.EncryptPassword(validateAccountDTO.Password, out var salt);

            account.Username = validateAccountDTO.Username;
            account.Password = hash;
            account.Salt = salt;

            await dataContext.SaveChangesAsync();
        }
        else
        {
            return BadRequest();
        }


        dataContext.Codes.Remove(code);
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

    private async Task<(Code, int)> GetCode(Guid userId)
    {
        var code = await dataContext.Codes.FirstOrDefaultAsync(c => c.UserId == userId);
        var generatedCode = code.GeneratedCode;

        return (code, generatedCode);
    }
};