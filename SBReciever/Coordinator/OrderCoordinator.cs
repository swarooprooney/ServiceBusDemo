using SBHelper.Receiver;
using SBHelper.Receiver.ExternalServices;
using SBShared.Models;
using System;
using System.Threading.Tasks;

namespace SBReceiver.Coordinator
{
    public class OrderCoordinator : IOrderCoordinator
    {
        private readonly ITopicReceiver<Order> _receiver;
        public OrderCoordinator(ITopicReceiver<Order> receiver)
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

        private void Receiver_RaiseMessageReadyEvent(object sender, Order order)
        {
            Console.WriteLine($"The order {order.OrderId} with name {order.OrderName} has been consumed"); ;
        }
    }
}
