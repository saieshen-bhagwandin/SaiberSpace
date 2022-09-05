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
        private readonly IOrderService _orderService;

        public EmailService(IConfiguration configuration, DataContext context,IOrderService orderService)
        {
           _configuration = configuration;
            _context = context;
            _orderService = orderService;
        }

       

        public void purchasedorder(EmailDTO useremail)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(useremail.user.Email));
            email.Subject = "Your SaiberSpace order confirmed ";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "<head><style> td { text-align:center; }</style ></head><h3>Thank you for your order!</h3 ><h4> Here's what you'll get : </h4><br>" + theitems(useremail)
            };

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
            email.Body = new TextPart(TextFormat.Html) { Text = "Hello new user, use the verication code : <br><br>" + user.VerificationToken + " to verify your new account" };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 25, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }



        public string theitems(EmailDTO email) {

            string textBody = " <table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 0 + " width = " + 600 + "><tr bgcolor='white'><th>Order Number</th><th>Product</th> <th>Edition</th><th>Quantity</th><th>Price</th></tr>";

            foreach (var item in email.cartItem)
            {

                textBody += "<tr><td>" + email.Ordernumber + "</td><td>" + item.ProductTitle + "</td><td> " + item.EditionName + "</td><td>" + item.Quantity + "</td><td>" + "R" + item.Price * item.Quantity + "</td></tr>";
            };


            textBody += "<tr><th> Total ( " + email.cartItem.Count + ")</th><td></td><td></td><td></td><th>" + "R" + email.cartItem.Sum(item => item.Price * item.Quantity) + "</th></tr>";

            textBody += "</table>";


            return textBody;

        }

    }
}
