using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Settings;
using System.Net;
using System.Net.Mail;

namespace GreenOcean.Business.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings emailSettings;

    public EmailService(EmailSettings emailSettings)
    {
        this.emailSettings = emailSettings;
    }

    public bool SendEmail(string recipientEmailAddress, string emailBody, string subject)
    {
        try
        { 
            var senderEmail = new MailMessage();
            senderEmail.From = new MailAddress(emailSettings.FromEmail);
            senderEmail.Subject = subject;
            var recipientEmail = new MailAddress(recipientEmailAddress);
            senderEmail.To.Add(recipientEmail);
            senderEmail.Body = emailBody;
            senderEmail.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailSettings.FromEmail, emailSettings.EmailPassword),
                EnableSsl = true
            };

            smtpClient.Send(senderEmail);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}