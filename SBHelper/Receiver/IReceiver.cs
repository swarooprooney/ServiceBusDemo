using System;
using System.Threading.Tasks;

namespace SBHelper.Receiver
{
    public interface IReceiver<T>
    {
        Task ReadMessageAsync(string queueName);
        Task CloseQueueAsync();
        event EventHandler<T> RaiseMessageReadyEvent;
        event EventHandler<ExceptionModel> RaiseExceptionEvent;
    }
}
