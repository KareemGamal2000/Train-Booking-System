using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Domain.Helpers;

namespace Domain.Services.Auth.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                _logger.LogInformation($"محاولة إرسال بريد إلى: {toEmail}");

                using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                    EnableSsl = _emailSettings.EnableSsl,
                    Timeout = 30000
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName, Encoding.UTF8),
                    Subject = subject,
                    SubjectEncoding = Encoding.UTF8,
                    Body = body,
                    BodyEncoding = Encoding.UTF8, 
                    IsBodyHtml = true,
                    HeadersEncoding = Encoding.UTF8 
                };

                mailMessage.To.Add(new MailAddress(toEmail));

                await client.SendMailAsync(mailMessage);

                _logger.LogInformation($"✅ تم إرسال البريد بنجاح إلى: {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ خطأ: {ex.Message}");
                throw new Exception($"فشل إرسال البريد: {ex.Message}", ex);
            }
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            var subject = "إعادة تعيين كلمة المرور - نظام السكك الحديدية";
            var body = $@"
<!DOCTYPE html>
<html lang='ar' dir='rtl'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
    <title>إعادة تعيين كلمة المرور</title>
</head>
<body style='font-family: Cairo, Tahoma, Arial, sans-serif; background-color: #f4f4f4; padding: 20px; direction: rtl;'>
    <div style='max-width: 600px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
        <h2 style='color: #4F46E5; text-align: center; font-size: 24px;'>🔒 إعادة تعيين كلمة المرور</h2>
        <p style='font-size: 16px; color: #333; line-height: 1.6;'>مرحباً،</p>
        <p style='font-size: 16px; color: #333; line-height: 1.6;'>لقد تلقينا طلباً لإعادة تعيين كلمة المرور الخاصة بحسابك.</p>
        <p style='font-size: 16px; color: #333; font-weight: bold;'>الرمز الخاص بك هو:</p>
        <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 25px; text-align: center; border-radius: 8px; margin: 20px 0;'>
            <h1 style='color: white; margin: 0; font-size: 36px; letter-spacing: 8px; font-weight: bold;'>{resetToken}</h1>
        </div>
        <p style='color: #EF4444; font-weight: bold; font-size: 15px;'>⚠️ هذا الرمز صالح لمدة 15 دقيقة فقط.</p>
        <p style='font-size: 14px; color: #666; line-height: 1.6;'>إذا لم تطلب إعادة تعيين كلمة المرور، يرجى تجاهل هذه الرسالة.</p>
        <hr style='border: none; border-top: 1px solid #e5e7eb; margin: 30px 0;'>
        <p style='color: #6B7280; font-size: 12px; text-align: center;'>مع تحيات فريق نظام السكك الحديدية 🚂</p>
    </div>
</body>
</html>";

            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendEmailChangeConfirmationAsync(string newEmail, string confirmationToken)
        {
            var subject = "تأكيد تغيير البريد الإلكتروني - نظام السكك الحديدية";
            var body = $@"
<!DOCTYPE html>
<html lang='ar' dir='rtl'>
<head>
    <meta charset='UTF-8'>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
</head>
<body style='font-family: Cairo, Tahoma, Arial, sans-serif; background-color: #f4f4f4; padding: 20px; direction: rtl;'>
    <div style='max-width: 600px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px;'>
        <h2 style='color: #10B981; text-align: center;'>✉️ تأكيد البريد الإلكتروني الجديد</h2>
        <p style='font-size: 16px; color: #333;'>مرحباً،</p>
        <p style='font-size: 16px; color: #333;'>رمز التأكيد الخاص بك:</p>
        <div style='background: linear-gradient(135deg, #10B981 0%, #059669 100%); padding: 25px; text-align: center; border-radius: 8px; margin: 20px 0;'>
            <h1 style='color: white; margin: 0; font-size: 36px; letter-spacing: 8px;'>{confirmationToken}</h1>
        </div>
        <p style='color: #EF4444; font-weight: bold;'>⚠️ صالح لمدة 15 دقيقة</p>
    </div>
</body>
</html>";

            await SendEmailAsync(newEmail, subject, body);
        }

        public async Task SendPasswordChangedNotificationAsync(string toEmail)
        {
            var subject = "تم تغيير كلمة المرور - نظام السكك الحديدية";
            var body = $@"
<!DOCTYPE html>
<html lang='ar' dir='rtl'>
<head>
    <meta charset='UTF-8'>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
</head>
<body style='font-family: Cairo, Tahoma, Arial, sans-serif; direction: rtl;'>
    <div style='max-width: 600px; margin: 0 auto; background: white; padding: 30px;'>
        <h2 style='color: #EF4444; text-align: center;'>🔔 تنبيه أمني</h2>
        <p style='font-size: 16px; color: #333;'>تم تغيير كلمة المرور بنجاح.</p>
        <p style='font-size: 14px; color: #666;'>التاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}</p>
    </div>
</body>
</html>";

            await SendEmailAsync(toEmail, subject, body);
        }
    }
}