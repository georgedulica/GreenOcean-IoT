using GreenOcean.Data;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GreenOcean.Controllers;

[ApiController]
public class ValidateAccountController : ControllerBase
{
    private readonly DataContext dataContext;

    public ValidateAccountController(DataContext dataContext)
    {
        this.dataContext = dataContext;
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
            var hash = EncryptPassword(validateAccountDTO.Password, out var salt);

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

    private byte[] EncryptPassword(string password, out byte[] salt)
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        return hash;
    }

    private async Task<(Code, int)> GetCode(Guid userId)
    {
        var code = await dataContext.Codes.FirstOrDefaultAsync(c => c.UserId == userId);
        var generatedCode = code.GeneratedCode;

        return (code, generatedCode);
    }
};