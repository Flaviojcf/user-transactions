namespace UserTransactions.Consumer.Services.EmailService
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string message);
    }
}
