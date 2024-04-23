using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace GreenOcean.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginToken>> Login(LoginDTO loginDTO)
    {
        var loginToken = await _loginService.Login(loginDTO);
        if (loginToken == null)
        {
            return Unauthorized("The username or password are not correct");
        }

        return loginToken;
    }
}