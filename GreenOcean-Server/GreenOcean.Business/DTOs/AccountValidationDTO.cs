namespace GreenOcean.Business.DTOs;

public class AccountValidationDTO
{
    public int Code { get; set; }

    public string Username { get; set; }
    
    public string Password { get; set; }

    public string ConfirmedPassword { get; set; }
}