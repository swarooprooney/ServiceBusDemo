using ServiceBusHelper.Enums;

namespace ServiceBusHelper.Sender.InternalServices
{
    public class SenderFactory
    {
        private readonly string _connectionString;

        public SenderFactory(string connectionString)
        {
           _connectionString = connectionString;
        }
        internal ISender GetConcreteSender(ServiceBusType type, string queueOrTopicName)
        {
            ISender sender;
            switch (type)
            {

                case ServiceBusType.Queue:
                    sender = new QueueService(_connectionString, queueOrTopicName);
                    break;
                case ServiceBusType.Topic:
                    sender = new TopicService(_connectionString, queueOrTopicName);
                    break;
                default:
                    sender = null;
                    break;
            }
            return sender;
        }
    }
}
