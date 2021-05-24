using System.Threading.Tasks;

namespace SBHelper.Sender.ExternalServices
{
    public interface ISenderService
    {
        Task SendMessage<T>(T message, string name, ServiceBusType type);
    }
}