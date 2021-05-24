using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SBHelper.Sender.InternalServices
{
    internal class QueueService : ISender
    {
        private static IQueueClient client;

        public QueueService(string connectionString,string queueName)
        {
            client = new QueueClient(connectionString, queueName);
        }

         async Task ISender.SendMessage<T>(T serviceBusMessage)
        {
            var messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await client.SendAsync(message);
        }

    }
}
