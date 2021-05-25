using System;
using System.Threading.Tasks;

namespace SBHelper.Receiver.ExternalServices
{
    public interface IReceiver<T>
    {
        Task ReadMessageAsync();
        Task CloseQueueAsync();
        event EventHandler<T> RaiseMessageReadyEvent;
        event EventHandler<ExceptionModel> RaiseExceptionEvent;
    }
}
