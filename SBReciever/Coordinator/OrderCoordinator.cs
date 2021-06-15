using SBHelper.Receiver;
using SBHelper.Receiver.ExternalServices;
using SBShared.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SBReceiver.Coordinator
{
    public class OrderCoordinator : IOrderCoordinator
    {
        private readonly ITopicReceiver _receiver;
        public OrderCoordinator(ITopicReceiver receiver)
        {
            _receiver = receiver;
        }

        public async Task CloseQueueAsync()
        {
            _receiver.RaiseMessageReadyEvent -= Receiver_RaiseMessageReadyEvent;
            _receiver.RaiseExceptionEvent -= Receiver_RaiseExceptionEvent;
            await _receiver.CloseQueueAsync();
        }

        public async Task GetMessageFromTopicAsync()
        {
            _receiver.RaiseMessageReadyEvent += Receiver_RaiseMessageReadyEvent;
            _receiver.RaiseExceptionEvent += Receiver_RaiseExceptionEvent;
            await _receiver.ReadMessageAsync();
        }

        private void Receiver_RaiseExceptionEvent(object sender, ExceptionModel e)
        {
            throw e.Exception;
        }

        private void Receiver_RaiseMessageReadyEvent(object sender, string jsonString)
        {
            Order order = JsonSerializer.Deserialize<Order>(jsonString);
            Console.WriteLine($"The order {order.OrderId} with name {order.OrderName} has been consumed"); ;
        }
    }
}
