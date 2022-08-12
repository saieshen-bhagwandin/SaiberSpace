using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        [HttpPost]
        public IActionResult SendEmail(string body) {

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("noel.west94@ethereal.email"));
                email.To.Add(MailboxAddress.Parse("noel.west94@ethereal.email"));
                email.Subject = "TESTING SUBJECT";
                email.Body = new TextPart(TextFormat.Html) { Text = body };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.ethereal.email", 465, SecureSocketOptions.StartTls);
                smtp.Authenticate("noel.west94@ethereal.email", "sCtsgx22vnFfxntCxT");
                smtp.Send(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());

            }
            return Ok();

        }


    }
}
