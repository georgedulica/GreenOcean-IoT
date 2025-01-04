using GreenOcean.Business.DTOs;
using GreenOcean.Business.Tokens;

namespace GreenOcean.Business.Interfaces;

public interface ILoginService
{
    public Task<LoginToken?> Login(LoginDTO loginDTO);
}
