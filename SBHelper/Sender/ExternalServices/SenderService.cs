using SBHelper.Sender.InternalServices;
using SBShared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SBHelper.Sender.ExternalServices
{
    public class SenderService : ISenderService
    {
        private readonly string _connectionString;

        public SenderService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SendMessage<T>(T message, string name, ServiceBusType type)
        {
            ISender sender = new SenderFactory(_connectionString).GetConcreteSender(type, name);
            await sender.SendMessage<T>(message);
        }
    }
}
