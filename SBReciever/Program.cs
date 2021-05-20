using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SBReceiver.Coordinator;
using SBReceiver.GenericReceiver;
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
            .AddLogging(opt=>
            {
                opt.AddConsole();
                opt.SetMinimumLevel(LogLevel.Debug);
            })
            .AddScoped<IReceiver<PersonModel>,Receiver<PersonModel>>()
            .AddScoped<IPersonCoordinator,PersonCoordinator>()
            .BuildServiceProvider();

            var consumer = serviceProvider.GetService<IPersonCoordinator>();
            await consumer.GetMessageFromQueueAsync();
            Console.ReadLine();

            await consumer.CloseQueueAsync();

        }
    }
    
}
