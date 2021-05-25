using System.Threading.Tasks;

namespace SBReceiver.Coordinator
{
    public interface IOrderCoordinator
    {
        Task CloseQueueAsync();
        Task GetMessageFromTopicAsync();
    }
}