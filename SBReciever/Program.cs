using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SBHelper;
using SBHelper.Receiver;
using SBHelper.Receiver.ExternalServices;
using SBReceiver.Coordinator;
using SBShared.Models;
using System;
using System.Threading.Tasks;

namespace SBReciever
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serviceProvider = new ServiceCollection()
            .AddLogging(opt =>
            {
                opt.AddConsole();
                opt.SetMinimumLevel(LogLevel.Debug);
            })
            .AddSingleton<IQueueReceiver<PersonModel>>(r => new QueueReceiver<PersonModel>("Endpoint=sb://swaroop.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LanCbMbi28mRgiCi+43BBgAel81+8PpZ11uPbaJTueI=", "personqueue"))
            .AddScoped<IPersonCoordinator, PersonCoordinator>()
            .AddSingleton<ITopicReceiver<Order>>(o => new TopicReceiver<Order>("Endpoint=sb://swaroop.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LanCbMbi28mRgiCi+43BBgAel81+8PpZ11uPbaJTueI=", "ecommerce", "Orders"))
            .AddScoped<IOrderCoordinator,OrderCoordinator>()
            .BuildServiceProvider();

            var consumer = serviceProvider.GetService<IOrderCoordinator>();
            await consumer.GetMessageFromTopicAsync();
            Console.ReadLine();

            await consumer.CloseQueueAsync();

        }
    }

}
