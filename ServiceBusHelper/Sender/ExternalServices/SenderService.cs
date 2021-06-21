using ServiceBusHelper.Enums;
using ServiceBusHelper.Sender.InternalServices;
using System.Threading.Tasks;

namespace ServiceBusHelper.Sender.ExternalServices
{
    public class SenderService : ISenderService
    {
        private readonly string _connectionString;

        public SenderService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SendMessageAsync<T>(T message, string queueOrTopicName, ServiceBusType type)
        {
            ISender sender = new SenderFactory(_connectionString).GetConcreteSender(type, queueOrTopicName);
            await sender.SendMessage<T>(message);
        }
    }
}
