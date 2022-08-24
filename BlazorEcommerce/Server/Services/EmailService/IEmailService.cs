namespace BlazorEcommerce.Server.Services.EmailService
{
    public interface IEmailService
    {

        void SendEmail(User user);

         void purchasedorder(EmailDTO email);

        

    }
}
