using Azure.Communication.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Identity.Client;

namespace Phonebook.Services
{

    public class AzureEmailService : IEmailSender
    {
        private readonly EmailClient _emailClient;
        private readonly string _senderAddress;

        public AzureEmailService(EmailClient emailClient, IConfiguration config)
        {
            _emailClient = emailClient;

            _senderAddress = config["EmailSettings:SenderEmail"] ?? throw new ArgumentNullException("SenderAddress configuration is missing");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<Program>() // Loads secrets.json for the specified assembly
                .Build();
            var azureConfig = builder.GetSection("EmailSettings");

            var confidentialClient = ConfidentialClientApplicationBuilder.Create(azureConfig["ApplicationId"])
                .WithClientSecret(azureConfig["ClientSecret"])
                .WithTenantId(azureConfig["TenantId"])
                .Build();
            string[] scopes = { "https://graph.microsoft.com/.default" };
            //Acquire the Access Token
            var authResult = await confidentialClient.AcquireTokenForClient(scopes).ExecuteAsync();
            var accessToken = authResult.AccessToken;
            await _emailClient.SendAsync(
                wait: Azure.WaitUntil.Started,
                senderAddress: _senderAddress,
                recipientAddress: email,
                subject: subject,
                htmlContent: htmlMessage);
        }
    }

}

