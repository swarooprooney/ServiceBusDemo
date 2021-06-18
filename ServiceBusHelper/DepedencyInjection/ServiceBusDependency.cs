using Microsoft.Extensions.DependencyInjection;
using ServiceBusHelper.Receiver.ExternalServices;
using ServiceBusHelper.Sender.ExternalServices;

namespace ServiceBusHelper.DepedencyInjection
{
    public static class ServiceBusDependency
    {
        public static IServiceCollection RegisterSender(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ISenderService>(p => new SenderService(connectionString));
            return services;
        }
        public static IServiceCollection RegisterRecieverForQueue(this IServiceCollection services, string connectionString,string queueName)
        {
            services.AddSingleton<IQueueReceiver>(r => new QueueReceiver(connectionString, queueName));
            return services;
        }

        public static IServiceCollection RegisterRecieverForTopic(this IServiceCollection services, string connectionString, string topicName,string subscriptionName)
        {
            services.AddSingleton<ITopicReceiver>(r => new TopicReceiver(connectionString, topicName, subscriptionName));
            return services;
        }
    }
}
