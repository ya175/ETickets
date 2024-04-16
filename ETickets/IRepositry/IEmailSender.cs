namespace ETickets.IRepositry
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}