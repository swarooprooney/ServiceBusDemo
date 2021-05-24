using System.Threading.Tasks;

namespace SBHelper.Sender.InternalServices
{
    internal interface ISender
    {
        Task SendMessage<T>(T serviceBusMessage);
    }
}
