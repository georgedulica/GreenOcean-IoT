namespace GreenOcean.Interfaces;

public interface IEmailService
{
    public bool SendRegistrationEmail(string name, string email, string code, string path);
}