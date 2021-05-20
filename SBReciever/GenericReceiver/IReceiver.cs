using System;
using System.Threading.Tasks;

namespace SBReceiver.GenericReceiver
{
    public interface IReceiver<T>
    {
        Task ReadMessageAsync(string queueName);
        Task CloseQueueAsync();

        event EventHandler<T> RaiseMessageReadyEvent;
    }
}