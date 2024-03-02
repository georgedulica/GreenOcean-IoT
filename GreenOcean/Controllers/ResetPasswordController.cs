using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Controllers;

[ApiController]
[Route("resetpassword")]
public class ResetPasswordController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IEmailService emailService;
    private readonly ITokenService tokenService;
    private readonly ISettingPassword settingPassword;

    public ResetPasswordController(DataContext dataContext, ITokenService tokenService, 
        IEmailService emailService, ISettingPassword settingPassword)
    {
        this.dataContext = dataContext;
        this.tokenService = tokenService;
        this.emailService = emailService;
        this.settingPassword = settingPassword;
    }

    [HttpPost]
    public async Task<ActionResult<ResetPasswordDTO>> SendEmail(EmailDTO emailDTO)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Email, emailDTO.Email));

        if (user == null)
        {
            return Unauthorized();
        }

        var generatedCode = GenerateCode();
        var code = new Code
        {
            GeneratedCode = generatedCode,
            UserId = user.Id
        };

        await dataContext.Codes.AddAsync(code);
        await dataContext.SaveChangesAsync();

        var sentEmail = emailService
            .SendRegistrationEmail(user.FirstName, user.Email, generatedCode.ToString(), "Templates/ResetPasswordTemplateEmailHTML.html");

        if (sentEmail == false)
        {
            return StatusCode(500);
        }

        var tokenDTO = new ResetPasswordDTO
        {
            Name = "reset",
            Token = tokenService.CreateToken(user.Username)
        };

        return tokenDTO;
    }

    [HttpPost("confirmcode")]
    public async Task<ActionResult<Guid>> ConfirmCode(CodeDTO codeDTO)
    {
        var recievedCode = codeDTO.Code;
        var (id, code) = await CompareCode(recievedCode);

        if (code == true)
        {
            return id;
        }
        else
        {
            return BadRequest();
        }
    }    
    
    [HttpPost("confirmcode/changepassword/{id}")]
    public async Task<IActionResult> ChangePassword(PasswordDTO passwordDTO, string id)
    {
        var password = passwordDTO.Password;
        var confirmedPassword = passwordDTO.ConfirmedPassword;

        if (!Guid.TryParse(id, out Guid userId))
        {
            return BadRequest("Invalid id format");
        }

        if (!string.Equals(password, confirmedPassword))
        {
            return BadRequest();
        }

        var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var hash = settingPassword.EncryptPassword(password, out var salt);
        
        user.Password = hash;
        user.Salt = salt;

        await dataContext.SaveChangesAsync();

        return Ok();
    }

    private int GenerateCode()
    {
        var random = new Random();
        var randomNumber = random.Next(100000, 1000000);

        return randomNumber;
    }

    private async Task<(Guid?, bool)> CompareCode(int recievedCode)
    {
        var code = await dataContext.Codes.FirstOrDefaultAsync(c => c.GeneratedCode == recievedCode);
        var id = code.UserId;

        if (code != null)
        {
            dataContext.Codes.Remove(code);
            await dataContext.SaveChangesAsync();

            return (id, true);
        }
        else
        {
            return (null, false);
        }
    }
}