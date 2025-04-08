using GreenOcean.Common.Enums;

namespace GreenOcean.Business.DTOs;

public class UserDTO
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }
}