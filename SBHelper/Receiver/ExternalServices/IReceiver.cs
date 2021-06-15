using System;
using System.Threading.Tasks;

namespace SBHelper.Receiver.ExternalServices
{
    public interface IReceiver
    {
        Task ReadMessageAsync();
        Task CloseQueueAsync();
        event EventHandler<string> RaiseMessageReadyEvent;
        event EventHandler<ExceptionModel> RaiseExceptionEvent;
    }
}
