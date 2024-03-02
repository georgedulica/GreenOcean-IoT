using GreenOcean.DTOs;

namespace GreenOcean.Interfaces;

public interface ITokenService
{
    public string CreateToken(string name);
}