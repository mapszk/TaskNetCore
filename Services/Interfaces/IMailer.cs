namespace TaskApp.Services.Interfaces
{
    public interface IMailer
    {
        Task Send(string to, string subject, string html, string? from = null);
    }
}