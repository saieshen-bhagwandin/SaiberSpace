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

       

        public void purchasedorder(EmailDTO useremail)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(useremail.user.Email));
            email.Subject = "Your SaiberSpace order confirmed ";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "<h3>Thank you for your order!</h3 ><h4> Here's what you'll get : </h4>" + theitems(useremail) };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 25, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);



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



        public string theitems(EmailDTO email) {


            string thing = "";

            thing = thing + "<ul>";
                
                foreach (var item in email.cartItem)
            {

                thing = thing + "<li> " +  item.ProductTitle + "-" + item.EditionName + "(" + "x" + item.Quantity + ")                          R" + item.Price * item.Quantity + " </li>";
           
           
           
           };


            thing = thing + "</ul>";


            return thing;


        }

    }
}
