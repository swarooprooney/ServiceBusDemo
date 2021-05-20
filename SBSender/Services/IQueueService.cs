using System.Threading.Tasks;

namespace SBSender.Services
{
    public interface IQueueService
    {
        Task SendMessage<T>(T serviceBusMessage, string queueName);
    }
}