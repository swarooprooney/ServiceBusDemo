using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SBHelper.Receiver.ExternalServices
{
    public class QueueReceiver<T> : IQueueReceiver<T>
    {

        private static IQueueClient queueClient;
        private readonly string _connectionString;
        public event EventHandler<T> RaiseMessageReadyEvent;
        public event EventHandler<ExceptionModel> RaiseExceptionEvent;
        public QueueReceiver(string connectionString,string queueName)
        {
            _connectionString = connectionString;
            queueClient = new QueueClient(_connectionString, queueName);
        }

        public async Task ReadMessageAsync()
        {
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
