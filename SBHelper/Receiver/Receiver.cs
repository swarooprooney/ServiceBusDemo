using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SBHelper.Receiver
{
    public class Receiver<T> : IReceiver<T>
    {

        private IQueueClient queueClient;
        private readonly string _connectionString;
        public event EventHandler<T> RaiseMessageReadyEvent;
        public event EventHandler<ExceptionModel> RaiseExceptionEvent;
        public Receiver(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ReadMessageAsync(string queueName)
        {
            queueClient = new QueueClient(_connectionString, queueName);
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
            RaiseMessageReadyEvent?.Invoke(this, JsonSerializer.Deserialize<T>(jsonString));
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            RaiseExceptionEvent?.Invoke(this, ExceptionConverter.ConvertToExceptionModel(arg));
            await Task.CompletedTask;
        }
    }
}
