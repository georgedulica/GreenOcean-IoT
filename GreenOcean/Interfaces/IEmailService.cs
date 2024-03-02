using GreenOcean.DTOs;

namespace GreenOcean.Interfaces;

public interface IEmailService
{
    public bool SendRegistrationEmail(UserDTO userDTO, string code, string path);
}