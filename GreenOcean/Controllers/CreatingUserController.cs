using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using GreenOcean.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GreenOcean.Controllers;

[ApiController]
public class CreatingUserController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IEmailService emailService;
    private readonly IOptions<EmailPathSettings> emailPathSettings;
    private readonly IOptions<EmailSubjectSettings> emailSubjectSettings;

    public CreatingUserController(DataContext context, IEmailService emailService,
        IOptions<EmailPathSettings> emailPathSettings, IOptions<EmailSubjectSettings> emailSubjectSettings)
    {
        this.dataContext = context;
        this.emailService = emailService;
        this.emailPathSettings = emailPathSettings;
        this.emailSubjectSettings = emailSubjectSettings;
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser(UserDTO userDTO)
    {
        if (userDTO.Email == null && userDTO.LastName == null && userDTO.FirstName == null)
        {
            return BadRequest();
        }

        var existingEmail = EmailExists(userDTO.Email);
        if (existingEmail == true)
        {
            return BadRequest();
        }

        var user = await SaveUser(userDTO);
        if (user == null)
        {
            return BadRequest();
        }

        var userId = user.Id;
        var code = await SaveCode(userId);
        var generatedCode = code.GeneratedCode.ToString();

        string emailTemplate = System.IO.File.ReadAllText(emailPathSettings.Value.RegistrationEmailPath);
        string emailBody = emailTemplate.Replace("{name}", user.FirstName)
                                        .Replace("{code}", generatedCode)
                                        .Replace("{id}", userId.ToString());

        var sentEmail = emailService.SendEmail(user.Email, emailBody, emailSubjectSettings.Value.RegistrationEmailSubject);
        if (sentEmail == false)
        {
            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();
            dataContext.Codes.Remove(code);
            await dataContext.SaveChangesAsync();
            return BadRequest("The email was not sent");
        }

        return Ok();
    }

    private async Task<User> SaveUser(UserDTO userDTO)
    {
        if (userDTO.Email != null && userDTO.LastName != null && userDTO.FirstName != null)
        {
            var randomUsername = BitConverter.ToString(GetRandomBlob()).Replace("-", "");
            var randomPassword = GetRandomBlob();
            var randomSalt = GetRandomBlob();

            var user = new User
            {
                Username = randomUsername,
                Password = randomPassword,
                Salt = randomSalt,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName
            };

            await dataContext.AddAsync(user);
            await dataContext.SaveChangesAsync();

            var userId = user.Id;
            return user;
        }
        else
        {
            return null;
        }
    }

    private async Task<Code> SaveCode(Guid userId)
    {
        var randomNumber = GenerateCode();

        var code = new Code
        {
            GeneratedCode = randomNumber,
            UserId = userId
        };

        await dataContext.AddAsync(code);
        await dataContext.SaveChangesAsync();

        return code;
    }

    private bool EmailExists(string email)
    {
        var user = dataContext.Users.Any(u => string.Equals(u.Email, email));
        if (user == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private byte[] GetRandomBlob()
    {
        var buffer = new byte[128];
        new Random().NextBytes(buffer);

        return buffer;
    }

    private int GenerateCode()
    {
        var random = new Random();
        var randomNumber = random.Next(100000, 1000000);

        return randomNumber;
    }
}