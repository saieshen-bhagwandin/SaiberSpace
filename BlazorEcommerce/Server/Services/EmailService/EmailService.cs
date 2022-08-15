using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace BlazorEcommerce.Server.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public EmailService(IConfiguration configuration, DataContext context)
        {
           _configuration = configuration;
            _context = context;
        }

        public  void SendEmail(User user)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "SaiberSpace - Verfication";
            email.Body = new TextPart(TextFormat.Html) { Text = "Hello new user, use the verication code : " + user.VerificationToken + " to verify your new account" };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 25, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
