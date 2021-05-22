using System;
using System.Threading.Tasks;

namespace SBHelper
{
    public interface IReceiver<T>
    {
        Task ReadMessageAsync(string queueName);
        Task CloseQueueAsync();

        event EventHandler<T> RaiseMessageReadyEvent;
    }
}
