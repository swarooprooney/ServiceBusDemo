using ServiceBusHelper.Enums;
using System.Threading.Tasks;

namespace ServiceBusHelper.Sender.ExternalServices
{
    public interface ISenderService
    {
        Task SendMessage<T>(T message, string queueOrTopicName, ServiceBusType type);
    }
}