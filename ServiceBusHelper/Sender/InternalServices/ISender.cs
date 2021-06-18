using System.Threading.Tasks;

namespace ServiceBusHelper.Sender.InternalServices
{
    internal interface ISender
    {
        Task SendMessage<T>(T serviceBusMessage);
    }
}
