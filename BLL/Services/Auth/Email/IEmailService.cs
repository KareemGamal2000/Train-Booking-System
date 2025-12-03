using System.Threading.Tasks;

namespace Domain.Services.Auth.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendPasswordResetEmailAsync(string toEmail, string resetToken);
        Task SendEmailChangeConfirmationAsync(string newEmail, string confirmationToken);
        Task SendPasswordChangedNotificationAsync(string toEmail);
    }
}