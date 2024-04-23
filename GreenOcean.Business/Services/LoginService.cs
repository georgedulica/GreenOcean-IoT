using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Tokens;
using GreenOcean.Data.Interfaces;

namespace GreenOcean.Business.Services;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly ITokenService _tokenService;

    public LoginService(ILoginRepository loginRepository, ITokenService tokenService)
    {
        _loginRepository = loginRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginToken?> Login(LoginDTO loginDTO)
    {
        try
        {
            var username = loginDTO.Username;
            var password = loginDTO.Password;

            var role = await _loginRepository.ExistsUser(username, password);
            if (role == null)
            {
                return null; 
            }

            var token = _tokenService.CreateLoginToken(loginDTO.Username, role);
            var loginToken = new LoginToken
            {
                Username = username,
                Token = token
            };

            return loginToken;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new ArgumentException($"{ex}");
        }
    }
}
