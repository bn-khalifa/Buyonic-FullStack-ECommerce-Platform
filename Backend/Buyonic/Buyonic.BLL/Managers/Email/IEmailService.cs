namespace Buyonic.BLL
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string body);
    }
}