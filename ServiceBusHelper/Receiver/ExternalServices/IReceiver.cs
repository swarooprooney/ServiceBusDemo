using System;
using System.Threading.Tasks;

namespace ServiceBusHelper.Receiver.ExternalServices
{
    public interface IReceiver
    {
        Task ReadMessageAsync();
        Task CloseQueueAsync();
        event EventHandler<string> RaiseMessageReadyEvent;
        event EventHandler<ExceptionModel> RaiseExceptionEvent;
    }
}
