using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GreenOcean.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly ITokenService tokenService;

    public LoginController(DataContext dataContext, ITokenService tokenService)
    {
        this.dataContext = dataContext;
        this.tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDTO>> Login(LoginDTO loginDTO)
    {
        bool login;
        
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, loginDTO.Username));
        if (user != null)
        {
            login = VerifyPassword(loginDTO.Password, user.Password, user.Salt); 
        }
        else
        {
            return Unauthorized();
        }

        if (login == true)
        {
            var tokenDTO = new TokenDTO
            {
                Name = user.Username,
                Token = tokenService.CreateToken(user.Username)
            };

            return tokenDTO;
        }
        else
        {
            return Unauthorized();
        }
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
    }
}