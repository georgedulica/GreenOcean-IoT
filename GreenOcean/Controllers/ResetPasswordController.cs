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
    private readonly ITokenService tokenService;

    public ResetPasswordController(DataContext dataContext, ITokenService tokenService)
    {
        this.dataContext = dataContext;
        this.tokenService = tokenService;
    }

    [HttpPost]
    public async Task<ActionResult<TokenDTO>> SendEmail(EmailDTO emailDTO)
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

        var tokenDTO = new TokenDTO
        {
            Name = emailDTO.Email,
            Token = tokenService.CreateToken(user.Username)
        };

        return tokenDTO;
    }

    private int GenerateCode()
    {
        var random = new Random();
        var randomNumber = random.Next(100000, 1000000);

        return randomNumber;
    }
}