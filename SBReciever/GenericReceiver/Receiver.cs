using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SBReceiver.GenericReceiver
{
    public class Receiver<T> : IReceiver<T>
    {
        private readonly IConfiguration _config;
        private IQueueClient queueClient;
        public event EventHandler<T> RaiseMessageReadyEvent;
        private T messageFromQueue;
        public Receiver()
        {
            //_config = config;
        }

        public async Task ReadMessageAsync(string queueName)
        {
            queueClient = new QueueClient("Endpoint=sb://swarooprooney.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=VCFeDPX1B93k3dHhr9ZvsZbspFUB7M2rT5Q+tvIR3Dw=", queueName);
            var messageOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(MessageHandler, messageOptions);
            await Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await queueClient.CloseAsync();
        }

        private async Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            messageFromQueue = JsonSerializer.Deserialize<T>(jsonString);
            Console.WriteLine(messageFromQueue);
            RaiseMessageReadyEvent?.Invoke(this, messageFromQueue);
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            throw arg.Exception;
        }
    }
}
