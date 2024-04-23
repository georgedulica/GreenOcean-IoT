namespace GreenOcean.Business.Interfaces;

public interface IEmailService
{
    public bool SendEmail(string recipientEmailAddress, string emailBody, string subject);
}