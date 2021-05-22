using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SBHelper;
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
            .AddScoped<IReceiver<PersonModel>>(r => new Receiver<PersonModel>("Endpoint=sb://swarooprooney.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=VCFeDPX1B93k3dHhr9ZvsZbspFUB7M2rT5Q+tvIR3Dw="))
            .AddScoped<IPersonCoordinator, PersonCoordinator>()
            .BuildServiceProvider();

            var consumer = serviceProvider.GetService<IPersonCoordinator>();
            await consumer.GetMessageFromQueueAsync();
            Console.ReadLine();

            await consumer.CloseQueueAsync();

        }
    }

}
