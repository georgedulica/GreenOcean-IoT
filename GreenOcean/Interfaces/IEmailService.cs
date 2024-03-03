namespace GreenOcean.Interfaces;

public interface IEmailService
{
    public bool SendRegistrationEmail(Guid? id, string name, string email, string code, string path);
}