using GreenOcean.Interfaces;
using System.Net.Mail;
using System.Net;
using GreenOcean.Settings;
using GreenOcean.DTOs;

namespace GreenOcean.Helpers;

public class EmailService : ICreateUser
{
    private readonly EmailSettings emailSettings;

    public EmailService(EmailSettings emailSettings)
    {
        this.emailSettings = emailSettings;
    }

    public bool SendRegistrationEmail(UserDTO userDTO, string code)
    {
        try
        { 
            string emailTemplate = File.ReadAllText(emailSettings.TemplatePath);
            string emailBody = emailTemplate.Replace("{name}", userDTO.FirstName)
                                            .Replace("{code}", code);

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailSettings.FromEmail);
            mailMessage.Subject = "Inregistrare";
            mailMessage.To.Add(new MailAddress(userDTO.Email));
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