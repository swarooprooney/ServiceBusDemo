using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SBSender.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _config;

        public QueueService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessage<T>(T serviceBusMessage, string queueName)
        {
            var connectionString = _config.GetConnectionString("ServiceBusConnectionString");
            var client = new QueueClient(connectionString, queueName);
            var messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await client.SendAsync(message);
        }
    }
}
