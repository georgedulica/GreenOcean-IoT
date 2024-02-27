using GreenOcean.DTOs;

namespace GreenOcean.Interfaces;

public interface ICreateUser
{
    public bool SendRegistrationEmail(UserDTO userDTO, string code);
}