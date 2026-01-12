using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Phonebook.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Jerry Chavis", "eleanore.veum18@ethereal.email"));
            message.To.Add(new MailboxAddress("Jerry Chavis", "jerrychavis97@gmail.com"));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = messageBody };

            using (var client = new SmtpClient())
                try
                {
                    {
                        await client.ConnectAsync("smtp.ethereal.com", 587, SecureSocketOptions.StartTls);
                        await client.AuthenticateAsync("eleanore.veum18@ethereal.email", "umSKtvhvCeSFHGUr2s");
                        await client.SendAsync(message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending email" + ex.ToString());
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
        }
    }
}