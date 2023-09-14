using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        SmtpClient _smtpClient;
        string _email;

        public EmailService(string smtpServer, int port, string username, string password)
        {
            _smtpClient = new SmtpClient(smtpServer, port)
            {
                Credentials = new System.Net.NetworkCredential(username, password),
                EnableSsl = true
            };
            _email = username;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_email),
                To = { email },
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            return _smtpClient.SendMailAsync(mailMessage);
        }
    }
}