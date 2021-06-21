using ServiceBusHelper.Enums;
using System.Threading.Tasks;

namespace ServiceBusHelper.Sender.ExternalServices
{
    public interface ISenderService
    {
        Task SendMessageAsync<T>(T message, string queueOrTopicName, ServiceBusType type);
    }
}