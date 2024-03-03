using GreenOcean.Interfaces;
using GreenOcean.Settings;
using System.Net;
using System.Net.Mail;

namespace GreenOcean.Helpers;

public class EmailService : IEmailService
{
    private readonly EmailSettings emailSettings;

    public EmailService(EmailSettings emailSettings)
    {
        this.emailSettings = emailSettings;
    }

    public bool SendRegistrationEmail(Guid? id, string name, string email, string code, string path)
    {
        try
        { 
            string emailTemplate = File.ReadAllText(path);
            string emailBody = emailTemplate.Replace("{name}", name)
                                            .Replace("{code}", code)
                                            .Replace("{id}", id.ToString());

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailSettings.FromEmail);
            mailMessage.Subject = "Inregistrare";
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Body = emailBody;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailSettings.FromEmail, emailSettings.EmailPassword),
                EnableSsl = true
            };

            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }

        return true;
    }
}