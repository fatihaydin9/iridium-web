using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Iridium.Infrastructure.Utilities
{
    public class MailClient
    {
        private readonly string SmtpAddress;
        private readonly string SmtpUsername;
        private readonly string SmtpPassword;
        private readonly string SmtpMail;

        public MailClient(string smtpAddress, string smtpUsername, string smtpPassword, string smtpMail)
        {
            SmtpAddress = smtpAddress;
            SmtpUsername = smtpUsername;
            SmtpPassword = smtpPassword;
            SmtpMail = smtpMail;
        }
        
        public void SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient(SmtpAddress, 587))
                {
                    smtp.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
                    smtp.EnableSsl = true;

                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(SmtpMail);
                        message.To.Add(toAddress);
                        message.Subject = subject;
                        message.Body = body;

                        smtp.Send(message);
                    }
                }
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
            }
        }
    }
}
