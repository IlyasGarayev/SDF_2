using System;
using System.Net;
using System.Net.Mail;

public class MailSender
{
    private string _smtpServer;
    private int _smtpPort;
    private string _fromEmail;
    private string _fromPassword;

    public MailSender(string smtpServer, int smtpPort, string fromEmail, string fromPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _fromEmail = fromEmail;
        _fromPassword = fromPassword;
    }

    public void SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_fromEmail, _fromPassword),
                EnableSsl = true
            };

            smtpClient.Send(mailMessage);
            Console.WriteLine("Email göndərildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email göndərilmədi: {ex.Message}");
        }
    }
}
