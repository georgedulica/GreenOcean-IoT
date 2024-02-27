using GreenOcean.Data;
using GreenOcean.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GreenOcean.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly DataContext dataContext;
    
    public LoginController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        bool login;
        
        var user = await dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, loginDTO.Username));
        if (user != null)
        {
            login = VerifyPassword(loginDTO.Password, user.Password, user.Salt); 
        }
        else
        {
            return BadRequest();
        }

        if (login == true)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
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