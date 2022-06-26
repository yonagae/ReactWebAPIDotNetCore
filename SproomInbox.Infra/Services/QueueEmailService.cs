using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using SproomInbox.Domain.Services;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Infra.Services
{
    public class QueueEmailService : IEmailService
    {
        private readonly IConfiguration _config;
        const string queueName = "emailqueue";

        public QueueEmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string recipient, string body)
        {
            var queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBus"), queueName);
            var message = new Message(Encoding.UTF8.GetBytes(body));

            await queueClient.SendAsync(message);
        }
    }
}
