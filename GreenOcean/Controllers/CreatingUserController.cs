using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Enums;
using GreenOcean.Interfaces;
using GreenOcean.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GreenOcean.Controllers;

[ApiController]
[Authorize (Roles = "Admin")]
public class CreatingUserController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IEmailService emailService;
    private readonly IOptions<EmailPathSettings> emailPathSettings;
    private readonly IOptions<EmailSubjectSettings> emailSubjectSettings;

    public CreatingUserController(DataContext dataContext, IEmailService emailService,
        IOptions<EmailPathSettings> emailPathSettings, IOptions<EmailSubjectSettings> emailSubjectSettings)
    {
        this.dataContext = dataContext;
        this.emailService = emailService;
        this.emailPathSettings = emailPathSettings;
        this.emailSubjectSettings = emailSubjectSettings;
    }

    [HttpGet("getUserRoles")]
    public async Task<IEnumerable<string>> GetRoles()
    {
        var roles = Enum.GetValues(typeof(UserRole))
                        .Cast<UserRole>()
                        .Select(role => role.ToString()).ToList();
        return roles;
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser(UserDTO userDTO)
    {
        var existingEmail = EmailExists(userDTO.Email);
        if (existingEmail == true)
        {
            return BadRequest("This email already exists");
        }

        var user = await SaveUser(userDTO);
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
            dataContext.Codes.Remove(code);
            await dataContext.SaveChangesAsync();
        }

        return Ok();
    }

    private async Task<User?> SaveUser(UserDTO userDTO)
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
                LastName = userDTO.LastName,
                Role = userDTO.Role.ToString()
            };

            await dataContext.AddAsync(user);
            await dataContext.SaveChangesAsync();

            return user;
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
        var existingUser = dataContext.Users.Any(u => string.Equals(u.Email, email));
        return existingUser;
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