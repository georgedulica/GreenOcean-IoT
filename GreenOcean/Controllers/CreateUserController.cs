using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
public class CreateUserController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IEmailService createUser;

    public CreateUserController(DataContext context, IEmailService createUser)
    {
        this.dataContext = context;
        this.createUser = createUser;
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser(UserDTO userDTO)
    {
        if (userDTO.Email != null && userDTO.LastName != null && userDTO.FirstName != null)
        {
            var existingEmail = EmailExists(userDTO.Email);
            if (existingEmail == true)
            {
                return BadRequest();
            }

            var userId = await SaveUser(userDTO);
            string code;

            if (userId != null)
            {
                code = (await SaveCode(userId.Value)).ToString();
            }
            else
            {
                return BadRequest("The account was not created");
            }

            var sentEmail = createUser.SendRegistrationEmail(userDTO.FirstName, userDTO.Email, code, "Templates/RegistrationEmailHTML.html");
            if (sentEmail == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest("The email was not sent");
            }
        }
        else
        {
            return BadRequest("The account was not created");
        }
    }

    private async Task<Guid?> SaveUser(UserDTO userDTO)
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
            return userId;
        }
        else
        {
            return null;
        }
    }

    private async Task<int> SaveCode(Guid userId)
    {
        var randomNumber = GenerateCode();

        var code = new Code
        {
            GeneratedCode = randomNumber,
            UserId = userId
        };

        await dataContext.AddAsync(code);
        await dataContext.SaveChangesAsync();

        return randomNumber;
    }

    private bool EmailExists(string email)
    {
        var user = dataContext.Users.Any(u => u.Email == email);
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