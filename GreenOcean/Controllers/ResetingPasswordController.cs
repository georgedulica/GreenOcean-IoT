using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using GreenOcean.Settings;
using GreenOcean.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GreenOcean.Controllers;

[ApiController]
[Route("resetpassword")]
public class ResetingPasswordController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IEmailService emailService;
    private readonly ITokenService tokenService;
    private readonly ISettingPassword settingPassword;
    private readonly IOptions<EmailPathSettings> emailPathSettings;
    private readonly IOptions<EmailSubjectSettings> emailSubjectSettings;

    public ResetingPasswordController(DataContext dataContext, ITokenService tokenService, 
        IEmailService emailService, ISettingPassword settingPassword, IOptions<EmailPathSettings> emailPathSettings,
        IOptions<EmailSubjectSettings> emailSubjectSettings)
    {
        this.dataContext = dataContext;
        this.tokenService = tokenService;
        this.emailService = emailService;
        this.settingPassword = settingPassword;
        this.emailPathSettings = emailPathSettings;
        this.emailSubjectSettings = emailSubjectSettings;
    }

    [HttpPost]
    public async Task<ActionResult<ResetPasswordToken>> SendEmail(EmailDTO emailDTO)
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

        string emailTemplate = System.IO.File.ReadAllText(emailPathSettings.Value.PasswordResetEmailPath);
        string emailBody = emailTemplate.Replace("{name}", user.FirstName)
                                        .Replace("{code}", generatedCode.ToString())
                                        .Replace("{id}", user.Id.ToString());

        var sentEmail = emailService.SendEmail(user.Email, emailBody, emailSubjectSettings.Value.PasswordResetEmailSubject);
        if (sentEmail == false)
        {
            dataContext.Codes.Remove(code);
            await dataContext.SaveChangesAsync();

            return BadRequest();
        }

        var tokenDTO = new ResetPasswordToken
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

        if (code == false)
        {
            return BadRequest();
        }

        return id;
    }    
    
    [HttpPost("confirmcode/changepassword/{id}")]
    public async Task<IActionResult> ChangePassword(PasswordDTO passwordDTO, Guid id)
    {
        var password = passwordDTO.Password;
        var confirmedPassword = passwordDTO.ConfirmedPassword;

        if (!string.Equals(password, confirmedPassword))
        {
            return BadRequest();
        }

        var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
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

    private async Task<(Guid?, bool)> CompareCode(int receivedCode)
    {
        var code = await dataContext.Codes.FirstOrDefaultAsync(c => c.GeneratedCode == receivedCode);
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