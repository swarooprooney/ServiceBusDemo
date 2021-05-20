using System.Threading.Tasks;

namespace SBReceiver.Coordinator
{
    public interface IPersonCoordinator
    {
        Task GetMessageFromQueueAsync();

        Task CloseQueueAsync();
    }
}