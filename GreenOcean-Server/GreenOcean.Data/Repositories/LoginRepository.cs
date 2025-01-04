using GreenOcean.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GreenOcean.Data.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly DataContext _dataContext;
    public LoginRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<string?> ExistsUser(string username, string password)
    {
        try
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
            if (user == null)
            {
                return null;
            }

            var matchingPasswords = VerifyPassword(password, user.Password, user.Salt);
            if (matchingPasswords == false)
            {
                return null;
            }

            var role = user.Role;
            return role;
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"The user cannot be verified {ex}");
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