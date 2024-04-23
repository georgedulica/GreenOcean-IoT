using GreenOcean.Business.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace GreenOcean.Business.Services;

public class SettingPassword : ISettingPassword
{
    public byte[] EncryptPassword(string password, out byte[] salt)
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
}