using Microsoft.Azure.ServiceBus;

namespace ServiceBusHelper.Receiver
{
    public static class ExceptionConverter
    {
        public static ExceptionModel ConvertToExceptionModel(ExceptionReceivedEventArgs args)
        {
            var exceptionModel = new ExceptionModel
            {
                Exception = args.Exception,
                Action = args.ExceptionReceivedContext?.Action,
                ClientId = args.ExceptionReceivedContext?.ClientId,
                Endpoint = args.ExceptionReceivedContext?.Endpoint,
                EntityPath = args.ExceptionReceivedContext?.EntityPath
            };
            return exceptionModel;
        }
    }
}
