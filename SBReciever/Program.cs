using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SBReceiver.Coordinator;
using System;
using System.Threading.Tasks;
using ServiceBusHelper.DepedencyInjection;
namespace SBReciever
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var services = new ServiceCollection();
            services.AddLogging(opt =>
            {
                opt.AddConsole();
                opt.SetMinimumLevel(LogLevel.Debug);
            });
            services.RegisterRecieverForQueue("Endpoint=sb://swaroop.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LanCbMbi28mRgiCi+43BBgAel81+8PpZ11uPbaJTueI=", "personqueue");
            services.AddScoped<IPersonCoordinator, PersonCoordinator>();
            services.RegisterRecieverForTopic("Endpoint=sb://swaroop.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LanCbMbi28mRgiCi+43BBgAel81+8PpZ11uPbaJTueI=", "ecommerce", "Order");
            services.AddScoped<IOrderCoordinator, OrderCoordinator>();
            var serviceProvider = services.BuildServiceProvider();
            var consumer = serviceProvider.GetService<IOrderCoordinator>();
            await consumer.GetMessageFromTopicAsync();
            Console.ReadLine();

            await consumer.CloseQueueAsync();

        }
    }

}
